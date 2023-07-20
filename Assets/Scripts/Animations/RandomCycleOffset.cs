using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCycleOffset : MonoBehaviour
{
    [SerializeField] string _animName;
    Animator[] _animators;

    private void Awake()
    {
        _animators = GetComponentsInChildren<Animator>();

    }

    private void Start()
    {
        _animators.ToList().ForEach(x => x.Play(_animName, 0, Random.Range(0, x.GetCurrentAnimatorStateInfo(0).length)));
    }
}
