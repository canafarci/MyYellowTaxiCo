using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    Coroutine _fadeLoadRoutine;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public IEnumerator DelayedLoadScene(int index, float delay, Action callback = null)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
        if (callback != null)
            callback();
    }


    public void FadedLoadScene(int index, float delay = 0f, Action callback = null)
    {
        if (_fadeLoadRoutine != null)
            StopCoroutine(_fadeLoadRoutine);

        _fadeLoadRoutine = StartCoroutine(FadeLoadSceneRoutine(index, delay, callback));
    }

    IEnumerator FadeLoadSceneRoutine(int index, float delay = 0f, Action callback = null)
    {
        yield return new WaitForSeconds(delay);

        Fader fader = GameManager.Instance.Fader;

        if (fader.FadeRoutine != null)
            StopCoroutine(fader.FadeRoutine);

        Coroutine fadeRoutine = StartCoroutine(fader.FadeOut());
        fader.FadeRoutine = fadeRoutine;
        yield return fadeRoutine;
        if (callback != null)
            callback();
        LoadScene(index);
    }

}
