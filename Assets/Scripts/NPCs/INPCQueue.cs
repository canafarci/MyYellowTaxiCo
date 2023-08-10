using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.NPC
{
    public interface INPCQueue
    {
        public void AddToQueue(RiderNPC npc);
        public bool QueueIsFull();
    }
}
