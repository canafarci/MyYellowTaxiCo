using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Coroutine FadeRoutine;
    CanvasGroup _fader;
    private void Awake()
    {
        _fader = FindObjectOfType<CanvasGroup>();
    }

    private void Start()
    {
        FadeLoad();
    }

    private void FadeLoad()
    {
        if (FadeRoutine != null)
            StopCoroutine(FadeRoutine);

        FadeRoutine = StartCoroutine(FadeIn());
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoad;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoad;
    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        FadeLoad();
    }

    public IEnumerator FadeOut()
    {
        if (_fader == null) { yield break; }
        while (_fader.alpha < 0.99)
        {
            yield return new WaitForSeconds(.05f);
            _fader.alpha += 0.1f;
        }
    }

    public IEnumerator FadeIn()
    {
        if (_fader == null) { yield break; }
        while (_fader.alpha > 0)
        {
            yield return new WaitForSeconds(.05f);
            _fader.alpha -= 0.1f;
        }
    }
}
