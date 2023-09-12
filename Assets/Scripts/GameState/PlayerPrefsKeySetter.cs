using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Scripts
{
    public class PlayerPrefsKeySetter : MonoBehaviour
    {
        public void SetValueForFirstTutorialComplete()
        {
            PlayerPrefs.SetInt(Globals.START_TUTORIAL_COMPLETE, 1);
        }
    }
}
