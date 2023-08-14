using System;
using System.Collections;
using Ketchapp.MayoSDK;
using TaxiGame.NPC;
using UnityEngine;
namespace TaxiGame.Upgrades
{
    public class UpgradesFacade
    {
        private float _incomeModifier;
        private float _npcSpeed;
        private int _npcInventorySize;
        public event Action<int> OnNPCInventorySizeUpgrade;
        public event Action<float> OnNPCSpeedUpgrade;
        public void SetIncomeModifier(float incomeModifier) => _incomeModifier = incomeModifier;
        public float GetIncomeModifier() => _incomeModifier;
        public void SetNPCSpeed(float speed)
        {
            _npcSpeed = speed;
            OnNPCSpeedUpgrade?.Invoke(_npcSpeed);
        }
        public float GetNPCSpeed() => _npcSpeed;
        public void SetNPCInventorySize(int size)
        {
            _npcInventorySize = size;
            OnNPCInventorySizeUpgrade?.Invoke(_npcInventorySize);
        }
        public int GetNPCInventorySize() => _npcInventorySize;


        //TODO progression subsystem
        // void PostProgression(string identifier, int upgradeLevel)
        // {
        //     var data = new Ketchapp.MayoSDK.Analytics.Data();
        //     data.AddValue("UpgradeName", identifier);
        //     data.AddValue("Money", (int)GameManager.Instance.Resources.PlayerMoney);
        //     KetchappSDK.Analytics.CustomEvent("UpgradeBought", data);
        // }
    }
}