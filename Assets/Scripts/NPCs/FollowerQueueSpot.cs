using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.NPC
{
    public class FollowerQueueSpot : MonoBehaviour
    {
        [SerializeField] private FollowerQueueSpot _nextSpot = null;
        [SerializeField] private bool _isSitSpot = false;
        private Follower _follower = null;

        public void MoveFollower(Follower follower)
        {

        }


        public bool IsEmpty() => _follower == null;
    }
}
