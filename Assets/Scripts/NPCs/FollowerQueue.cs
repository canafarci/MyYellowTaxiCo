using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Taxi.NPC
{
    public class FollowerQueue : MonoBehaviour, INPCQueue
    {
        public void AddToQueue(NPCActionScheduler npc)
        {
            Follower follower = npc as Follower;

        }

        public void AddToQueue(RiderNPC npc)
        {
            throw new NotImplementedException();
        }

        public bool QueueIsFull()
        {
            throw new NotImplementedException();
        }
    }

}