using System.Collections;
using System.Collections.Generic;
using TaxiGame.WaitZones;
using TMPro;
using UnityEngine;

namespace TaxiGame.UI
{
    public class RepeatableBuyableWaitingZoneVisual : BuyableWaitingZoneVisual
    {
        [SerializeField] TextMeshProUGUI _levelText;
        private void Start()
        {
            RepeatableBuyingWaitingZone buyableWaitingZone = _waitZone as RepeatableBuyingWaitingZone;
            Initialize(buyableWaitingZone.GetCost());
        }

        public void SetLevelText(int currentIndex)
        {
            _levelText.text = "LEVEL " + (currentIndex + 1).ToString();
        }
    }
}
