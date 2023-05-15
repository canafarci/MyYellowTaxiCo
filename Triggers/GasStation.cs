using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasStation : MonoBehaviour
{
    public Slider Slider;
    public Renderer Renderer;
    public Animator Animator;
    public GameObject Thunder,StationFX,CircleUI;
    [SerializeField] Transform _handle, _startPos;
    public bool IsActive = false;
    private void Start() => _handle.position = _startPos.position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !IsActive)
        {
            _handle.parent = GameManager.Instance.References.PlayerHand;
            _handle.localPosition = Vector3.zero;
            _handle.localRotation = Quaternion.Euler(Vector3.zero);
            GameManager.Instance.References.PlayerAnimator.SetWalking();
            _handle.GetComponent<Handle>().IsActive = true;
            IsActive = true;
            Thunder.SetActive(true);
            CircleUI.SetActive(false);
        }
    }
}
