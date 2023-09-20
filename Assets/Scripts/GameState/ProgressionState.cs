using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.GameState
{
    public class ProgressionState : IInitializable
    {
        //Dependencies
        private GameProgressModel _gameProgressionModel;
        //Variables
        private Dictionary<UnlockSequence, string> _unlockSequenceToKeyMap;
        //Events
        //Subscribed from ObjectiveArrow Visual class
        public event EventHandler<OnFirstVIPSpawnedArgs> OnFirstVIPSpawned;

        public ProgressionState(GameProgressModel progressionModel)
        {
            _gameProgressionModel = progressionModel;
        }
        public void Initialize()
        {
            _unlockSequenceToKeyMap = new Dictionary<UnlockSequence, string>
            {
                { UnlockSequence.StartTutorial, Globals.START_TUTORIAL_COMPLETE },
                { UnlockSequence.ChargeTutorial, Globals.FIRST_CHARGER_TUTORIAL_COMPLETE },
                { UnlockSequence.EngineRepairTutorial, Globals.SECOND_BROKEN_TUTORIAL_COMPLETE },
                { UnlockSequence.TireChangeTutorial, Globals.THIRD_TIRE_TUTORIAL_COMPLETE },
                { UnlockSequence.CustomerTutorial, Globals.FOURTH_CUSTOMER_TUTORIAL_COMPLETE },
                { UnlockSequence.VIPTutorial, Globals.FIFTH_VIP_TUTORIAL_COMPLETE }
            };
        }

        public bool IsTutorialSequenceComplete(UnlockSequence sequence)
        {
            return PlayerPrefs.HasKey(_unlockSequenceToKeyMap[sequence]);
        }
        public void HandleStartTutorialComplete()
        {
            PlayerPrefs.SetInt(Globals.START_TUTORIAL_COMPLETE, 1);
        }
        public void HandleVIPTriggered()
        {
            if (!IsTutorialSequenceComplete(UnlockSequence.VIPTutorial))
            {
                _gameProgressionModel.VIPTriggered();
            }
        }
        public void HandleVIPSpawned(Wanderer wanderer)
        {
            if (!IsTutorialSequenceComplete(UnlockSequence.VIPTutorial))
            {
                _gameProgressionModel.OnFirstVIPSpawned(wanderer);
                OnFirstVIPSpawned?.Invoke(this, new OnFirstVIPSpawnedArgs { Target = wanderer.transform });
            }
        }
    }

    public class OnFirstVIPSpawnedArgs : EventArgs
    {
        public Transform Target;
    }

    public enum UnlockSequence
    {
        StartTutorial,
        ChargeTutorial,
        EngineRepairTutorial,
        TireChangeTutorial,
        CustomerTutorial,
        VIPTutorial
    }
}
