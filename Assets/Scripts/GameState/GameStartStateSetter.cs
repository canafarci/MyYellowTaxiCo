using TaxiGame.GameState.Unlocking;
using TaxiGame.Resource;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace TaxiGame.GameState
{
    public class GameStartStateSetter : MonoBehaviour
    {
        [SerializeField] private UnityEvent _gameStartEvent;
        private SequentialUnlockable _unlockable;
        private MoneyStacker _stacker;
        private ProgressionState _progressionState;
        private const int START_MONEY_COUNT = 48;

        [Inject]
        private void Init(SequentialUnlockable unlockable, MoneyStacker stacker, ProgressionState progressionState)
        {
            _unlockable = unlockable;
            _stacker = stacker;
            _progressionState = progressionState;
        }
        private void Start()
        {
            if (!PlayerPrefs.HasKey(Globals.FIRST_TIME_GAME_STARTED))
            {
                _gameStartEvent.Invoke();
                _stacker.StackMoney(START_MONEY_COUNT);
            }
            else if (!_progressionState.IsStartTutorialComplete())
            {
                _unlockable.UnlockSequentially();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!PlayerPrefs.HasKey(Globals.FIRST_TIME_GAME_STARTED) && other.CompareTag(Globals.PLAYER_TAG))
            {
                PlayerPrefs.SetInt(Globals.FIRST_TIME_GAME_STARTED, 1);
                _unlockable.UnlockObject();
            }
        }
    }
}
