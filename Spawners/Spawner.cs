using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Car Car { get { return _currentCar; } }
    public Action OnMovedAway;
    public GameObject[] BrokenCars { set { _brokenCars = value; } }
    [SerializeField] bool _spawnAlternate;
    [SerializeField] Transform _enterNode, _exitNode;
    [SerializeField] GameObject _item;
    [SerializeField] GameObject[] _brokenCars;
    [SerializeField] Animator _parkAnimator;
    [SerializeField] int _everyNthIsBroken = 2;
    [SerializeField] MoneyStacker _stacker;
    [SerializeField] bool _specialChargerSpawn, _specialBrokenSpawn, _specialTireSpawn;
    Car _currentCar = null;
    RoadNode _nodes;
    SpawnerUI _ui;
    int _spawnIndex = 0;

    private void Awake()
    {
        _nodes = FindObjectOfType<RoadNode>();
        _ui = GetComponent<SpawnerUI>();
    }
    private void Start() => InitialSpawn();

    void OnCarInPlace(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
    {
        if (!IsBrokenCar)
            _currentCar = car;
        if (_spawnIndex > 0)
            FindObjectsOfType<SpawnDriver>().Where(x => x.HatType == hatType).FirstOrDefault().
                                                                                DriverSpawn(transform);
    }

    void OnCarRepaired(Car car) => _currentCar = car;

    void OnCarExited(Car car)
    {
        car.CarInPlaceHandler -= OnCarInPlace;
        car.CarExitedHandler -= OnCarExited;
        car.CarExitedHandler -= OnCarRepaired;
        SpawnCar();
    }

    public void StartMove()
    {
        StartCoroutine(_ui.WaitLoop());
        _currentCar.TakeOffFX();
        _parkAnimator.Play("ParkOut");
        _spawnIndex += 1;
    }

    public void CallMove()
    {
        _currentCar.MoveAway();
        _currentCar = null;
        OnMovedAway();
        _parkAnimator.enabled = false;
    }
    public void SpawnCar()
    {
        Car car;

        if (_specialChargerSpawn && !PlayerPrefs.HasKey(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE))
        {
            car = GameObject.Instantiate(_brokenCars[0],
                                 _nodes.SpawnNode.position, _nodes.SpawnNode.rotation).
                                 GetComponent<Car>();

            car.CarInPlaceHandler += FirstChargerReturn;

        }
        else if (_specialBrokenSpawn && !PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
        {
            car = GameObject.Instantiate(_brokenCars[0],
                                 _nodes.SpawnNode.position, _nodes.SpawnNode.rotation).
                                 GetComponent<Car>();

            car.CarInPlaceHandler += FirstBrokenReturn;

        }
        else if (_specialTireSpawn && !PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
        {
            car = GameObject.Instantiate(_brokenCars[0],
                                 _nodes.SpawnNode.position, _nodes.SpawnNode.rotation).
                                 GetComponent<Car>();

            car.CarInPlaceHandler += FirstTireReturn;
        }
        else
        {
            car = GameObject.Instantiate(_spawnIndex % _everyNthIsBroken == 0 || _spawnIndex == 0 ?
                                 _brokenCars[UnityEngine.Random.Range(0, _brokenCars.Length)] :
                                 _item,
                                 _nodes.SpawnNode.position, _nodes.SpawnNode.rotation).
                                 GetComponent<Car>();
            car.CarInPlaceHandler += OnCarInPlace;
        }

        //Start
        car.EnterNode = _enterNode;
        car.ExitNode = _nodes.EndNode;
        car.parkAnimator = _parkAnimator;
        car.Stacker = _stacker;

        //sub to actions
        car.CarRepairedHandler += OnCarRepaired;
        car.CarExitedHandler += OnCarExited;
    }

    void InitialSpawn()
    {
        Car car = GameObject.Instantiate(_item, _nodes.StartNode.position, _nodes.StartNode.rotation).GetComponent<Car>();

        car.EnterNode = _enterNode;
        car.ExitNode = _nodes.EndNode;
        car.parkAnimator = _parkAnimator;
        car.Stacker = _stacker;

        car.GetComponent<Animator>().enabled = false;

        car.CarInPlaceHandler += OnCarInPlace;
        car.CarExitedHandler += OnCarExited;

        car.GetToParkPosition();
    }

    void FirstChargerReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
    {
        OnCarInPlace(car, IsBrokenCar, hatType);
        FindObjectOfType<ConditionalTutorial>().FirstReturnCarWithoutCharger();
    }
    void FirstBrokenReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
    {
        OnCarInPlace(car, IsBrokenCar, hatType);
        FindObjectOfType<ConditionalTutorial>().SecondReturnBroken();
    }
    void FirstTireReturn(Car car, bool IsBrokenCar, Enums.StackableItemType hatType)
    {
        OnCarInPlace(car, IsBrokenCar, hatType);
        FindObjectOfType<ConditionalTutorial>().ThirdReturnBroken();
    }
}
