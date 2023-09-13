using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visuals
{
    public class ItemRemoverVisual : MonoBehaviour
    {
        private ItemRemover _remover;

        [Inject]
        private void Init(ItemRemover remover)
        {
            _remover = remover;
        }

        private void Start()
        {
            _remover.OnItemRemoved += ItemRemover_ItemRemovedHandler;
        }

        private void ItemRemover_ItemRemovedHandler(object sender, OnItemRemovedArgs e)
        {
            Tween(e.Item, e.Target);
        }

        void Tween(Transform item, Transform target)
        {
            Vector3[] path = GeneratePath(item, target);

            item.transform.DOPath(path,
                                    .5f,
                                    PathType.CatmullRom,
                                    PathMode.Full3D)
                                    .onComplete = () => Destroy(item.gameObject, 0.1f);
        }

        private static Vector3[] GeneratePath(Transform item, Transform target)
        {
            return new Vector3[]
            {
                item.transform.position,
                (item.transform.position + target.position) / 2f + new Vector3(0f, 2f, 0f),
                target.position
            };
        }
    }
}