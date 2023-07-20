using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliAnimTrigger : MonoBehaviour
{
    [SerializeField] Animator _wobbleAnimator;
    [SerializeField] GameObject _npc;
    public void OnTakeOff()
    {
        _wobbleAnimator.enabled = true;
    }
    public void OnLanding()
    {
        _wobbleAnimator.enabled = false;
    }
    public void DisableNPC()
    {
        _npc.SetActive(false);
    }
}
