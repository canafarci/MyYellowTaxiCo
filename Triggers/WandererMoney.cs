using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class WandererMoney : MonoBehaviour
{
    public static event Action WandererMoneyPickupHandler;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) { return; }

        transform.parent = null;
        Vector3 endPos = other.transform.position;
        Vector3 intermediatePos = new Vector3((endPos.x + other.transform.position.x) / 2f,
                                                endPos.y + 2f,
                                                (endPos.z + other.transform.position.z) / 2f);
        Vector3[] path = { intermediatePos, endPos };
        transform.DOPath(path, .1f, PathType.CatmullRom, PathMode.Full3D);

        WandererMoneyPickupHandler.Invoke();
        Destroy(gameObject, 0.20f);
    }
}
