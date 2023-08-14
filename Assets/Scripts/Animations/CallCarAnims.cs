using System.Collections;
using System.Collections.Generic;
using TaxiGame.Vehicle;
using UnityEngine;

public class CallCarAnims : MonoBehaviour
{
    [SerializeField] CarSpawner _spawner;
    public void OnCarInPlace()
    {
        GetComponentInChildren<Car>().OnInPlace();
    }

    public void OnCarExit()
    {
        _spawner.CallMove();
    }

}
