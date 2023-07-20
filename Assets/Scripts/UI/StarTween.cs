using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StarTween : MonoBehaviour
{
    [SerializeField] Transform _target, _child;
    [SerializeField] TextMeshProUGUI _text, _textStar1, _textStar2;
    [SerializeField] Slider _slider;
    Vector3 _startPos;
    private void Start()
    {
        _startPos = transform.position;
    }
    public void OnLevelProgress(Action callback)
    {
        _child.gameObject.SetActive(true);

        transform.DOMove(_target.transform.position, 1.5f).SetEase(Ease.InSine);

        Sequence seq = DOTween.Sequence();
        int numOfMoves = UnityEngine.Random.Range(2, 5);
        float interval = 1.5f / (float)(numOfMoves + 1);

        for (int i = 0; i < numOfMoves; i++)
            seq.Append(_child.DOLocalMoveX(UnityEngine.Random.Range(100f, 200f) * Mathf.Pow(-1, i), interval).SetEase(Ease.InSine));

        seq.Append(_child.DOLocalMoveX(0, interval));

        seq.onComplete = () =>
        {
            callback();
            _child.gameObject.SetActive(false);
            transform.position = _startPos;
        };
    }

    public void UpdateUI(int level, int levelProgress, int levelIncreaseValue)
    {
        _slider.value = (float)levelProgress / (float)levelIncreaseValue;
        _text.text = levelProgress.ToString() + "/" + levelIncreaseValue.ToString();
        _textStar1.text = level.ToString();
        _textStar2.text = level.ToString();
    }
}
