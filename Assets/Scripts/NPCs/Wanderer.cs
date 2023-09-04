using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using TaxiGame.Items;

namespace TaxiGame.NPC
{
    public class Wanderer : MonoBehaviour, IInventoryObject
    {
        private List<Waypoint> _waypoints = new List<Waypoint>();
        private RiderNPCController _controller;
        private Follower _follower;

        [Inject]
        private void Create(Transform spawnTransform,
                            Waypoint[] waypoints,
                            RiderNPCController controller,
                            Follower follower)
        {
            transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
            _waypoints = waypoints.ToList();
            _controller = controller;
            _follower = follower;
        }

        private void Start()
        {
            CreateWanderingBehaviour();

            Destroy(gameObject, 180f);
        }

        private void CreateWanderingBehaviour()
        {
            foreach (Waypoint waypoint in _waypoints)
            {
                _controller.Move(waypoint.transform.position);

                if (waypoint.StopWaypoint)
                {
                    _controller.Wait(10f);
                }
            }
        }

        public InventoryObjectType GetObjectType() => InventoryObjectType.VIP;
        public Follower GetFollower() => _follower;
        public RiderNPCController GetController() => _controller;
        public class Factory : PlaceholderFactory<Object, Transform, Waypoint[], Wanderer>
        {
        }
    }
}