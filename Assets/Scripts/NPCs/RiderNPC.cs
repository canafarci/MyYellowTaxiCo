using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.NPC
{
    public class RiderNPC : MonoBehaviour
    {
        private RiderNPCView _view;
        public RiderNPCView GetView() => _view;

        [Inject]
        private void Init(RiderNPCView view)
        {
            _view = view;
        }

        [Inject]
        private void Create(Vector3 spawnPos, Quaternion rotation)
        {
            transform.position = spawnPos;
            transform.rotation = rotation;
        }

        public class Factory : PlaceholderFactory<Object, Vector3, Quaternion, RiderNPC>
        {
        }
    }
}
