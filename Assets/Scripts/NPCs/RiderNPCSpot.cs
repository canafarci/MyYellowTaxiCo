using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.NPC
{
    public class RiderNPCSpot : MonoBehaviour
    {
        RiderNPC _npc;
        public void SetNPC(RiderNPC npc) => _npc = npc;
        public bool HasNPC() => _npc != null;
        public void Clear() => _npc = null;
        public bool TryGetNPC(out RiderNPC npc)
        {
            npc = _npc;
            return npc != null;
        }

    }
}
