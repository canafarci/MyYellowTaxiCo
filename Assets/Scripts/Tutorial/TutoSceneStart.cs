using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSceneStart : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
       // TinySauce.OnGameStarted("Tutorial");
    }
}
