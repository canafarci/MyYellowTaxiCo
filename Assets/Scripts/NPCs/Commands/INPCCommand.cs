using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.NPC.Command
{
    public interface INPCCommand
    {
        public IEnumerator Execute();
    }
}
