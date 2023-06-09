using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCarAnims : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    public void OnCarInPlace()
    {
        GetComponentInChildren<Car>().OnInPlace();
    }

    public void OnCarExit()
    {
        _spawner.CallMove();
    }

}
