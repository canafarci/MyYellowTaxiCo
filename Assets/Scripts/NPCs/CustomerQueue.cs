using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Taxi.NPC
{
    public class CustomerQueue : MonoBehaviour, INPCQueue
    {
        [SerializeField] private CustomerQueueSpot _headNode;
        [SerializeField] private CustomerQueueSpot _endNode;

        public void AddToQueue(RiderNPC npc)
        {
            _headNode.MoveFollower(npc);
        }

        public bool QueueIsFull()
        {
            return !_headNode.IsEmpty();
        }

        public bool TryGetFollower(out Follower follower)
        {
            Customer customer = _endNode.GetNPC() as Customer;

            if (customer == null)
            {
                follower = null;
                return false;
            }
            else
            {
                follower = customer.GetFollower();
                _endNode.Clear();
                _endNode.ShiftQueue();
                return true;
            }
        }
    }

}