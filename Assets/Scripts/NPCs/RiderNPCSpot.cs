using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class RiderNPCSpot : MonoBehaviour
    {
        private RiderNPC _npc;
        public void SetNPC(RiderNPC npc) => _npc = npc;
        public RiderNPC GetNPC() => _npc;
        public bool IsEmpty() => _npc == null;
        public void Clear() => _npc = null;
    }
}
