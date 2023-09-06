using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.NPC
{
    public interface IHelperNPCState
    {
        public void Enter();
        public void Tick();
        public void Exit();
    }
}
