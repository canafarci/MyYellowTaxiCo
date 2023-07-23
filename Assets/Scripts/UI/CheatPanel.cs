using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class CheatPanel : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cam;
    [SerializeField] TextMeshProUGUI _text;
    private void Start()
    {
        GetComponentInChildren<Slider>(true).onValueChanged.AddListener(ChangeZoom);
    }
    public void OnGainButtonClicked()
    {
        ResourceTracker.Instance.OnCheatMoneyGain(1000f);
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
