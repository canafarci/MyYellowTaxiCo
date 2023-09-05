using TaxiGame.NPC.Command;
using UnityEngine;
using Zenject;

namespace TaxiGame.NPC
{
    public class RiderNPC : MonoBehaviour
    {
        private RiderNPCController _controller;
        public RiderNPCController GetController() => _controller;

        [Inject]
        private void Init(RiderNPCController controller)
        {
            _controller = controller;
        }

        [Inject]
        private void Create(Vector3 spawnPos, Quaternion rotation)
        {
            transform.SetPositionAndRotation(spawnPos, rotation);
        }

        public class Factory : PlaceholderFactory<Object, Vector3, Quaternion, RiderNPC>
        {
        }
    }
}
