using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.NPC;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Taxi.Animations
{
    public class NavMeshAnimator : MonoBehaviour
    {
        public Animator Animator { set { _animator = value; } }
        private NavMeshAgent _agent;
        private Animator _animator;
        private NavMeshNPC _npc;

        [Inject]
        private void Init(Animator animator, NavMeshAgent agent, NavMeshNPC npc)
        {
            _animator = animator;
            _agent = agent;
            _npc = npc;
        }

        private void Start()
        {
            _npc.OnNPCAnimationStateChanged += NavMeshNPC_NPCAnimationStateChangedHandler;
        }

        private void NavMeshNPC_NPCAnimationStateChangedHandler(object sender, OnNPCAnimationStateChangedArgs e)
        {
            _animator.SetBool(e.AnimationStateString, e.State);
        }

        private void FixedUpdate()
        {
            if (_animator == null)
                Debug.LogError("NO ANIMATOR ATTACHED TO THE NPC");

            _animator.SetFloat("Speed", Vector3.Magnitude(_agent.velocity) / _agent.speed);
        }
    }
}


