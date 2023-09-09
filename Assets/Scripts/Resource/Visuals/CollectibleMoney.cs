using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TaxiGame.Resource.Visuals
{
    public class CollectibleMoney : MonoBehaviour
    {
        //Rotation aligned orthagonally with the world
        private readonly Vector3 _startRotation = new Vector3(0f, 300f, 0f);
        private void Spawn(Vector3 position)
        {
            transform.SetPositionAndRotation(position, Quaternion.Euler(_startRotation));
        }

        public class Pool : MonoMemoryPool<Vector3, CollectibleMoney>
        {
            protected override void Reinitialize(Vector3 position, CollectibleMoney money)
            {
                money.Spawn(position);
            }
        }
    }
}
