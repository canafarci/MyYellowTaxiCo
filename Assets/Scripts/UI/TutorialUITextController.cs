using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaxiGame.UI
{
    public class TutorialUITextController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _tutorialTexts;
        public void DisableTutorialUI()
        {
            DisableAllTutorialTexts();
            gameObject.SetActive(false);
        }
        public void DisplayTutorialText(GameObject text)
        {
            gameObject.SetActive(true);
            DisableAllTutorialTexts();
            text.SetActive(true);
        }
        private void DisableAllTutorialTexts()
        {
            foreach (GameObject text in _tutorialTexts)
            {
                text.SetActive(false);
            }
        }
    }
}
