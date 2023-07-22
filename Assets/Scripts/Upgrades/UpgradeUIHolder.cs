using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
namespace Taxi.Upgrades
{
    //TODO move tu UI subsystem
    public class UpgradeUIHolder : MonoBehaviour
    {
        [SerializeField] GameObject _tutorialFader, _tutorialHand;
        [SerializeField] bool _isPlayerUI;
        RectTransform _rect;
        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, -1200f);
        }
        private void OnEnable()
        {
            _rect.DOLocalMoveY(-480.82f, 0.5f).SetEase(Ease.OutBack);

            SetTutorial();
        }

        private void SetTutorial()
        {
            if (_isPlayerUI)
            {
                if (PlayerPrefs.GetInt(Globals.PLAYER_INVENTORY_KEY) == 0)
                {
                    _tutorialFader.SetActive(true);
                    _tutorialHand.SetActive(true);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt(Globals.NPC_COUNT_KEY) == 0)
                {
                    _tutorialFader.SetActive(true);
                    _tutorialHand.SetActive(true);
                }
            }
        }

        public void DisableCanvas()
        {
            _rect.DOLocalMoveY(-1200f, 0.5f).SetEase(Ease.OutBack).onComplete = () =>
            {
                gameObject.SetActive(false);
            };
        }
    }
}