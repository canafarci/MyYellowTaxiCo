using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public void Start()
    {
        int index;
        if (PlayerPrefs.HasKey("TutorialComplete"))
        {
            index = 2;
        }
        else
        {
            index = 1;
           
        }
        StartCoroutine(DelayedLoadScene(index));
    }
    IEnumerator DelayedLoadScene(int index)
    {

       

        yield return new WaitForSeconds(3f);

        GameManager.Instance.SceneLoader.LoadScene(index);
    }

}
