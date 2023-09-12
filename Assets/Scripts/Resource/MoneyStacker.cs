using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Vehicles;
using UnityEngine;
using Zenject;

namespace TaxiGame.Resource
{
    public class MoneyStacker : MonoBehaviour
    {
        //Dependencies
        private IVehicleEvents _vehicleEvents;
        //Variables
        [SerializeField] private Transform _spawnPos;
        private int _moneyCountInStack;
        //Subscribed from MoneyStackerVisual
        public event Action OnMoneyAddedToStack;

        [Inject]
        private void Init([InjectOptional] IVehicleEvents spot)
        {
            _vehicleEvents = spot;
        }

        private void Start()
        {
            if (_vehicleEvents != null)
                _vehicleEvents.OnVehicleMoneyEarned += (val) => StackMoney(val);
        }

        public void StackMoney(int count)
        {
            StartCoroutine(StackItemRoutine(count));
        }

        private IEnumerator StackItemRoutine(int count)
        {
            while (count > 0)
            {
                _moneyCountInStack++;
                count--;
                OnMoneyAddedToStack?.Invoke();
                yield return new WaitForSeconds(Globals.TIME_STEP);
            }
        }
        //Getters-Setters
        public int GetMoneyCountInStack() => _moneyCountInStack;
        public void DecrementMoneyCountInStack() => _moneyCountInStack -= 1;
    }

}
