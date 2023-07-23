using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SceneLoader SceneLoader { get { return _sceneLoader; } }
    public CameraController CameraController { get { return _cameraController; } }
    public AudioPlayer AudioManager { get { return _audioManager; } }
    public Fader Fader { get { return _fader; } }
    public References References { get { return _references; } }
    SceneLoader _sceneLoader;
    GameStart _gameStart;
    CameraController _cameraController;
    AudioPlayer _audioManager;
    Fader _fader;
    References _references;

    public static GameManager Instance { get; private set; }
    void Awake()
    {
        transform.parent = null;

        if (Instance)
            Destroy(gameObject);
        else
        {
            Instance = this;
        }

        _sceneLoader = GetComponent<SceneLoader>();
        _gameStart = GetComponent<GameStart>();
        _cameraController = GetComponent<CameraController>();
        _audioManager = GetComponentInChildren<AudioPlayer>();
        _fader = GetComponent<Fader>();
        _references = GetComponent<References>();
    }

    private void OnEnable() => SceneManager.activeSceneChanged += OnSceneLoaded;
    private void OnDisable() => SceneManager.activeSceneChanged -= OnSceneLoaded;

    private void OnSceneLoaded(Scene arg0, Scene arg1)
    {
        _gameStart.enabled = true;
    }
}
