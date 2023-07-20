using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComplete : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.SceneLoader.FadedLoadScene(2, 13.5f);
        PlayerPrefs.SetInt("TutorialComplete", 1);
        //TinySauce.OnGameFinished(true, 100, "Tutorial");
    }
}
