using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshAnimator : MonoBehaviour
{
    public Animator Animator { set { _animator = value; } }
    NavMeshAgent _agent;
    Animator _animator;

    int _moveEmptyHash = Animator.StringToHash("MoveEmptyHands");
    int _idleHash = Animator.StringToHash("Idle");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (_animator == null) return;
        _animator.SetFloat("Speed", Vector3.Magnitude(_agent.velocity) / _agent.speed);
    }
}
