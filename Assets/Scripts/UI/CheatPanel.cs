using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
using Zenject;
using TaxiGame.Resource;

public class CheatPanel : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private TextMeshProUGUI _text;
    private ResourceTracker _resourceTracker;

    [Inject]
    private void Init(ResourceTracker tracker)
    {
        _resourceTracker = tracker;
    }

    private void Start()
    {
        GetComponentInChildren<Slider>(true).onValueChanged.AddListener(ChangeZoom);
    }


    public void OnGainButtonClicked()
    {
        _resourceTracker.OnCheatMoneyGain(1000f);
    }
    public void OnResetCameraButtonClicked()
    {
        _cam.m_Lens.OrthographicSize = 14f;
    }


    public void ChangeZoom(float value)
    {
        _cam.m_Lens.OrthographicSize = value;
        _text.text = value.ToString("F1");
    }
}
