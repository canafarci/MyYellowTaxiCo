using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickupFX : MonoBehaviour
{
    ParticleSystem _fx;
    private void Awake()
    {
        _fx = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        MoneyStacker.MoneyPickupHandler += OnMoneyPickup;
        WandererMoney.WandererMoneyPickupHandler += OnMoneyPickup;
    }


    private void OnDisable()
    {
        MoneyStacker.MoneyPickupHandler -= OnMoneyPickup;
        WandererMoney.WandererMoneyPickupHandler -= OnMoneyPickup;
    }
    private void OnMoneyPickup()
    {
        _fx.Play();
    }
}
