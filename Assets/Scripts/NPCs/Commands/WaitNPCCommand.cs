using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.NPC.Command
{
    public class WaitNPCCommand : INPCCommand
    {
        private float _duration;

        public WaitNPCCommand(float duration)
        {
            _duration = duration;
        }
        public virtual IEnumerator Execute()
        {
            yield return new WaitForSeconds(_duration);
        }
    }
}
