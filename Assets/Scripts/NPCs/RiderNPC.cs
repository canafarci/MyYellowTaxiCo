using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class RiderNPC : MonoBehaviour
    {
        private RiderNPCController _view;
        public RiderNPCController GetView() => _view;

        [Inject]
        private void Init(RiderNPCController view)
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
