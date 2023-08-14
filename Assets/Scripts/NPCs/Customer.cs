using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.NPC
{
    public class Customer : RiderNPC
    {
        private Follower _follower;
        public Follower GetFollower() => _follower;

        [Inject]
        private void Init(Follower follower)
        {
            _follower = follower;
        }
    }
}
