using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class CustomerQueueSpot : RiderNPCSpot
    {
        [SerializeField] private CustomerQueueSpot _nextSpot = null;
        [SerializeField] private CustomerQueueSpot _previousSpot = null;
        [SerializeField] private bool _isSitSpot = false;
        public void MoveFollower(RiderNPC npc)
        {
            if (_nextSpot != null && _nextSpot.IsEmpty())
            {
                _nextSpot.MoveFollower(npc);
            }
            else
            {
                if (_isSitSpot)
                {
                    npc.GetView().MoveAndSit(transform);
                }
                else
                {
                    npc.GetView().Move(transform);
                }
                SetNPC(npc);
            }
        }
        public void ShiftQueue()
        {
            if (_previousSpot != null && !_previousSpot.IsEmpty())
            {
                _previousSpot.MoveFollower(_previousSpot.GetNPC());
                _previousSpot.Clear();
                _previousSpot.ShiftQueue();
            }
        }
    }
}
