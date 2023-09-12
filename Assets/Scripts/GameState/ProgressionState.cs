using System.Collections;
using System.Collections.Generic;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.GameState
{
    public class ProgressionState
    {
        private GameProgressModel _gameProgressionModel;

        [Inject]
        private void Init(GameProgressModel progressionModel)
        {
            _gameProgressionModel = progressionModel;
        }

        public bool IsVIPTutorialComplete()
        {
            return PlayerPrefs.HasKey(Globals.FIFTH_VIP_TUTORIAL_COMPLETE);
        }
        public bool IsStartTutorialComplete()
        {
            return PlayerPrefs.HasKey(Globals.START_TUTORIAL_COMPLETE);
        }
        public void HandleStartTutorialComplete()
        {
            PlayerPrefs.SetInt(Globals.START_TUTORIAL_COMPLETE, 1);
        }
        public void HandleVIPTriggered()
        {
            if (!IsVIPTutorialComplete())
            {
                _gameProgressionModel.VIPTriggered();
            }
        }
        public void HandleVIPSpawned(Wanderer wanderer)
        {
            if (!IsVIPTutorialComplete())
                _gameProgressionModel.OnFirstWandererSpawned(wanderer);
        }
    }
}
