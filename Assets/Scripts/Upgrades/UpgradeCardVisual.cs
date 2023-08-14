using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace TaxiGame.Upgrades
{
    public class UpgradeCardVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private GameObject[] _upgradeDots;
        [SerializeField] private Button _button;
        public void UpdateDotUI(int index)
        {
            for (int i = 0; i < index && i < _upgradeDots.Length; i++)
            {
                _upgradeDots[i].SetActive(true);
            }
        }
        public void SetButtonInteractable(bool active)
        {
            Assert.IsNotNull(_button);
            _button.interactable = active;
        }
        public void SetCostText(float cost)
        {
            if (cost > 0f)
            {
                if (cost >= 1000)
                {
                    if (cost % 1000 == 0)
                        _costText.text = (cost / 1000).ToString("F0") + "K";
                    else
                        _costText.text = (cost / 1000).ToString("F1") + "K";
                }
                else
                {
                    _costText.text = cost.ToString("F0");
                }
            }
            else
                _costText.text = "FREE";
        }
        public void SetCostTextToMax()
        {
            _costText.text = "MAX";
        }
    }
}