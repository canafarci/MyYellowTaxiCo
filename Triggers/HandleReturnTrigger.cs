using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleReturnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Handle")) { return; }

        other.GetComponent<Handle>().Return();
    }
}
