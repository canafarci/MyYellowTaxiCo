using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Vehicles;
using UnityEngine;
using Zenject;

public class MoneyStacker : MonoBehaviour
{
    [SerializeField] Vector3 _moneyDimensions = new Vector3(.55f, .20f, .75f);
    Vector3 _lastPos;
    Transform _player;
    Stack<Transform> _moneyStack = new Stack<Transform>();
    [SerializeField] int _rows, _columns;
    [SerializeField] GameObject _prefab;
    [SerializeField] Transform _spawnPos;
    [SerializeField] string _identifier;
    int _currentRow, _currentColumn, _currentAisle = 0;
    private VehicleSpot _spot;

    public static event Action MoneyPickupHandler;

    [Inject]
    private void Init([InjectOptional(Id = "MoneyStacker")] VehicleSpot spot)
    {
        _spot = spot;
    }

    private void Awake()
    {
        _lastPos = new Vector3(_moneyDimensions.x / 2, 0, _moneyDimensions.z / 2);
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (String.IsNullOrEmpty(_identifier)) return;
        if (PlayerPrefs.HasKey(_identifier)) return;
        StackItems(48);
    }

    private void Start()
    {
        if (_spot != null)
            _spot.OnVehicleDeparted += (val) => StackItems(val);
    }

    public void StackItems(int count) => StartCoroutine(StackItemRoutine(count));
    public void StartEmptyStack() => StartCoroutine(EmptyStack());
    IEnumerator StackItemRoutine(int count)
    {
        if (count == 0)
            yield break;

        Transform money = GameObject.Instantiate(_prefab, _spawnPos.position, _prefab.transform.localRotation, transform).transform;

        Vector3 endPos = CalculatePos(_currentRow, _currentColumn, _currentAisle);
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 1f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };

        money.transform.DOLocalPath(path, .3f, PathType.CatmullRom, PathMode.Full3D);

        _moneyStack.Push(money);
        IncreaseStackDimensions();

        yield return new WaitForSeconds(.05f);
        StackItems(count - 1);
    }
    IEnumerator EmptyStack()
    {
        _currentRow = 0;
        _currentAisle = 0;
        _currentColumn = 0;

        Transform money;
        int count = _moneyStack.Count;
        while (_moneyStack.TryPop(out money))
        {
            yield return StartCoroutine(DotweenFX.MoneyArcTween(money, transform.position, count));
            MoneyPickupHandler.Invoke();
        }
    }

    Vector3 CalculatePos(int row, int column, int aisle)
    {
        return new Vector3((_moneyDimensions.x / 2) + ((row - 1) * _moneyDimensions.x),
                            _moneyDimensions.y * (aisle - 1),
                           (_moneyDimensions.z / 2) + ((column - 1) * _moneyDimensions.z)
                          );
    }

    void IncreaseStackDimensions()
    {
        if (_currentRow < _rows)
            _currentRow++;

        else if (_currentColumn < _columns)
        {
            _currentRow = 0;
            _currentColumn++;
        }
        else
        {
            _currentRow = 0;
            _currentColumn = 0;
            _currentAisle++;
        }
    }
}
