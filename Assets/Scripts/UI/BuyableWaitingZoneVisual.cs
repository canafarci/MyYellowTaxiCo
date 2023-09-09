using TMPro;
using UnityEngine;
using DG.Tweening;
using TaxiGame.WaitZones;
using UnityEngine.Assertions;
using Zenject;
using TaxiGame.Resource.Visuals;
using TaxiGame.Visuals;

namespace TaxiGame.UI
{
    public class BuyableWaitingZoneVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        protected WaitingEngine _waitZone;
        private IFeedbackVisual _fillable;
        private MoneyVisual.Pool _moneyVisualPool;
        private TweeningService _tweeningService;

        [Inject]
        private void Init([InjectOptional] IFeedbackVisual visual,
                          MoneyVisual.Pool moneyVisualPool,
                          TweeningService tweeningService)
        {
            _fillable = visual;
            _moneyVisualPool = moneyVisualPool;
            _tweeningService = tweeningService;
        }
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

            TweenMoneyVisual(e.Instigator);
        }

        private void TweenMoneyVisual(GameObject instigator)
        {
            Vector3 spawnPos = instigator.transform.position + Vector3.up * 1.5f;
            MoneyVisual moneyVisual = _moneyVisualPool.Spawn(spawnPos);

            moneyVisual.transform.parent = transform;
            Sequence moveSequence = _tweeningService.GenerateMoveSequenceWithRandomRotation(moneyVisual.transform, Vector3.zero, 0.2f);
            moveSequence.onComplete = () => _moneyVisualPool.Despawn(moneyVisual);
        }

        //can be called from upgrade command as well
        public void Initialize(float moneyToUnlock)
        {
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

            _fillable?.SetValue(0, 1);
        }
        //Cleanup
        private void OnDisable()
        {
            _waitZone.OnWaitEngineIteration -= UpdateVisual;
        }
    }
}