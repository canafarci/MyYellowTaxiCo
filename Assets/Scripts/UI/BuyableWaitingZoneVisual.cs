using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using TaxiGame.WaitZones;
using UnityEngine.Assertions;
using System;

namespace TaxiGame.UI
{
    public class BuyableWaitingZoneVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        protected WaitingEngine _waitZone;
        private IFeedbackVisual _fillable;
        private void Awake()
        {
            _waitZone = GetComponent<WaitingEngine>();
            Assert.IsNotNull(_waitZone);
        }
        private void OnEnable()
        {
            _waitZone.OnWaitEngineIteration += UpdateVisual;
        }
        private void Start()
        {
            BuyableWaitingZone buyableWaitingZone = _waitZone as BuyableWaitingZone;
            Initialize(buyableWaitingZone.GetCost());
        }
        private void UpdateVisual(object sender, WaitEngineIterationEventArgs e)
        {
            FormatText(e.CurrentValue);
            _text.DOColor(Color.green, 0.1f);
            _fillable?.SetValue(e.CurrentValue, e.MaxValue);
            DotweenFX.MoneyArcTween(transform.position);
        }
        //can be called from upgrade command as well
        public void Initialize(float moneyToUnlock)
        {
            _fillable = GetComponent<IFeedbackVisual>();

            if (_text != null)
                FormatText(moneyToUnlock);
        }
        private void FormatText(float value)
        {
            if (value >= 1000)
            {
                if (value % 1000 == 0)
                    _text.text = (value / 1000).ToString("F0") + "K";
                else
                    _text.text = (value / 1000).ToString("F1") + "K";
            }
            else
                _text.text = value.ToString("F0");
        }
        public void ResetVisual(float moneyToUnlock)
        {
            if (_text != null)
                FormatText(moneyToUnlock);

            if (_fillable != null)
                _fillable.SetValue(0, 1);
        }
        //Cleanup
        private void OnDisable()
        {
            _waitZone.OnWaitEngineIteration -= UpdateVisual;
        }
    }
}