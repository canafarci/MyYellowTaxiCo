using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TaxiGame.NPC
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

        public bool TryGetCustomer(out FollowingNPC customer)
        {
            customer = _endNode.GetNPC() as FollowingNPC;

            if (customer == null)
            {
                return false;
            }
            else
            {
                _endNode.Clear();
                _endNode.ShiftQueue();
                return true;
            }
        }
    }

}