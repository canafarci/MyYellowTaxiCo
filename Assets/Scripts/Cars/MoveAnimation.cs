using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    [SerializeField] Animator _animator;
    CarFX _carFX;
    private void Awake()
    {
        _carFX = GetComponent<CarFX>();
    }

    public void TakeOffFX()
    {
        _carFX.TakeOffFX();
        _animator.Play("WobbleParkOut");
    }
    public void ParkTween(Animator animator, Transform enterNode, Animator parkAnimator, GameObject _driver)
    {
        animator.enabled = false;


        Tween move = transform.DOMove(enterNode.position, 0.6f).SetEase(Ease.Linear);

        TweenCallback callback = () =>
        {
            parkAnimator.enabled = true;
            parkAnimator.Play("ParkIn");
            _animator.Play("WobbleParkIn");

            transform.parent = parkAnimator.transform;
            transform.localPosition = Vector3.zero;

        };
        move.onComplete = callback;
    }

    public void MoveAwayTween(Animator animator, Transform exitNode, Action onTakeOff, Car car)
    {
        transform.parent = null;
        onTakeOff();

        float refVel = 25f / 3f;
        float distance = Vector3.Distance(transform.position, exitNode.position);

        float time = distance / refVel;

        Tween move = transform.DOMove(exitNode.position, time).SetEase(Ease.Linear);

        TweenCallback callback = () =>
        {
            animator.enabled = true;
            animator.Play("CarExit");
            FindObjectOfType<LevelProgress>().OnLevelProgress();
        };
        move.onComplete = callback;
    }

    public void OnInPlace(GameObject _driver)
    {
        Tween scale = _driver.transform.DOScale(0.00001f, .5f);
        TweenCallback cb = () => { _driver.SetActive(false); };
        scale.onComplete = cb;
        _animator.Play("WobbleIdle");
    }
}
