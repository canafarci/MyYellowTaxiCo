using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.NPC
{
    public interface INPCQueue
    {
        public void AddToQueue(RiderNPC npc);
        public bool QueueIsFull();
    }
}
