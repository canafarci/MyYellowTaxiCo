using System;
using System.Collections;
using System.Collections.Generic;
using TaxiGame.Installers;
using TaxiGame.NPC;
using UnityEngine;
using Zenject;

namespace TaxiGame.Scripts
{
    public class HelperNPCStateMachine : MonoBehaviour
    {
        private IHelperNPCState _currentState;

        [Inject]
        private void Init([Inject(Id = HelperNPCStates.DeliverHat)] IHelperNPCState initialState)
        {
            _currentState = initialState;
        }
        private void Start()
        {
            _currentState.Enter();
        }
        private void Update()
        {
            _currentState.Tick();
        }
        public void TransitionTo(IHelperNPCState nextState)
        {
            _currentState.Exit();
            _currentState = nextState;
            _currentState.Enter();
        }
    }
}
