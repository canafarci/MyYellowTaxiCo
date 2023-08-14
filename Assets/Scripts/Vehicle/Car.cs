using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;
using TaxiGame.Vehicle;

public class Car : MonoBehaviour
{
    public Enums.StackableItemType ItemType { get { return _hatType; } }
    public Animator parkAnimator;
    public Transform EnterNode, ExitNode;
    public bool IsBrokenCar;
    public MoneyStacker Stacker;
    [SerializeField] private Transform[] _passengers;
    [SerializeField] private GameObject _driver;
    [SerializeField] private int _moneyToGain;
    [SerializeField] private Enums.StackableItemType _hatType;
    private Animator _animator;
    private MoveAnimation _moveTween;
    private Vector3 _baseDriverScale;
    public event Action<Car> CarExitedHandler, CarRepairedHandler;
    public event Action<Car, bool, Enums.StackableItemType> CarInPlaceHandler;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveTween = GetComponent<MoveAnimation>();
        _baseDriverScale = _driver.transform.localScale;
    }
    public void GetToParkPosition() => _moveTween.ParkTween(_animator, EnterNode, parkAnimator, _driver);
    public void MoveAway() => _moveTween.MoveAwayTween(_animator, ExitNode, () => Stacker.StackItem(_moneyToGain), this);
    public void TakeOffFX() => _moveTween.TakeOffFX();
    public void OnCarRepaired() => CarRepairedHandler?.Invoke(this);
    public void PreMoveFX(bool withPassengers)
    {
        _driver.SetActive(true);
        _driver.transform.DOScale(_baseDriverScale, .4f);

        if (!withPassengers) { return; }

        foreach (Transform tr in _passengers)
        {
            tr.gameObject.SetActive(true);
            Vector3 baseScale = tr.localScale;
            tr.localScale = Vector3.one * 0.00001f;
            tr.DOScale(baseScale, .4f);
        }
    }
    public void OnFinish()
    {
        CarExitedHandler?.Invoke(this);
        Destroy(gameObject, .2f);
    }
    public void OnInPlace()
    {
        _moveTween.OnInPlace(_driver);
        CarInPlaceHandler?.Invoke(this, IsBrokenCar, _hatType);
    }
}

public struct CarConfig
{
    public Animator ParkAnimator;
    public Transform EnterParkNode;
    public Transform ExitParkNode;
    public TaxiSpot TaxiSpot;

    public CarConfig(Animator parkAnimator,
            Transform enterParkNode,
            Transform exitNode,
            TaxiSpot spot)
    {
        EnterParkNode = enterParkNode;
        ExitParkNode = exitNode;
        ParkAnimator = parkAnimator;
        TaxiSpot = spot;
    }


}
