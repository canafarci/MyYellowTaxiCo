using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HatHelperAnimator : MonoBehaviour
{
    // Animations _animations;
    NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        //_animations = GetComponent<Animations>();
    }

    private void FixedUpdate()
    {
        if (_agent.velocity.magnitude > 0.1f)
        {
            //_animations.IsMoving = true;
        }
        else
        {
            //_animations.IsMoving = false;
        }
    }
}