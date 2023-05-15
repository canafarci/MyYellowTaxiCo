using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameObject _panel;
    public void SetText(string text)
    {
        _panel.SetActive(true);
        _text.text = text;
    }
    public void DisablePanel()
    {
        _panel.SetActive(false);
    }
}
