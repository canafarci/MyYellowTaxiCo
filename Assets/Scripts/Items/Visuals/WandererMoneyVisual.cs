using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Items.Visual
{
    public class WandererMoneyVisual : MonoBehaviour
    {
        private WandererMoney _wandererMoney;

        [Inject]
        private void Init(WandererMoney wandererMoney)
        {
            _wandererMoney = wandererMoney;
        }

        private void Start()
        {
            _wandererMoney.OnWandererMoneyPickedUp += WandererMoney_WandererMoneyPickedUpHandler;

            Vector3 target = new Vector3(transform.position.x, 0f, transform.position.z);

            MoveTween(target, 1f);
        }

        private void WandererMoney_WandererMoneyPickedUpHandler(object sender, OnWandererMoneyPickedUpArgs e)
        {
            MoveTween(e.Target.position, 0.2f);
        }

        private void MoveTween(Vector3 target, float duration)
        {
            transform.DOKill();

            transform.parent = null;

            Vector3[] path = {transform.position,
                            new Vector3(target.x, 2f, target.z),
                            new Vector3(target.x + GetNoise(), target.y, target.z + GetNoise())
                        };

            transform.DOPath(path, duration, PathType.CatmullRom);
            transform.DORotate(new Vector3(0f, UnityEngine.Random.Range(0f, 360f), 0f), duration);
        }

        private float GetNoise()
        {
            return UnityEngine.Random.Range(0f, 1f);
        }
    }
}
