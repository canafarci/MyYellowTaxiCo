using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CarGasFill : MonoBehaviour
{
    // [SerializeField] Transform _handlePos;
    // [SerializeField] GameObject _noGasIcon, _fullIcon, _chargeIcon, _emptyIcon, _chargeFX;
    // [SerializeField] Image _carSlider;
    // public bool CarIsRepaired = false;

    //public void AttachHandle(Handle handle, HosePump hose, GasStation station)
    //{
    //    StartCoroutine(FillGasLoop(handle, hose, station));
    //}

    //IEnumerator FillGasLoop(Handle handle, HosePump hose, GasStation station)
    //{
    //    GameObject StationFX = station.StationFX;
    //    Slider slider = station.Slider;
    //    Renderer renderer = station.Renderer;
    //    Animator animator = station.Animator;

    //    handle.transform.parent = null;
    //    Tween handleMovement = handle.transform.DOMove(_handlePos.position, 1f);
    //    handle.transform.DORotate(_handlePos.rotation.eulerAngles, 1f);

    //    GameManager.Instance.References.PlayerAnimator.ResetWalking();

    //    yield return handleMovement.WaitForCompletion();
    //    //StationFX.SetActive(true);
    //    //hose.enabled = true;
    //    //animator.enabled = true;
    //    //renderer.material = GameManager.Instance.References.GameConfig.ThunderWithEmission;
    //    _chargeFX.SetActive(true);

    //    //DOTween.To(() => slider.value, x => slider.value = x, 1, 3);
    //    Tween tween = DOTween.To(() => _carSlider.fillAmount, x => _carSlider.fillAmount = x, 1, 3);

    //    yield return tween.WaitForCompletion();

    //    _fullIcon.SetActive(true);
    //    _chargeIcon.SetActive(false);
    //    _emptyIcon.SetActive(false);

    //    yield return new WaitForSeconds(1f);

    //    _noGasIcon.SetActive(false);



    //    //StationFX.SetActive(false);
    //    //animator.enabled = false;
    //    //renderer.material = GameManager.Instance.References.GameConfig.ThunderWithoutEmission;
    //    //slider.value = 0;
    //    //hose.baseThickness = 0.075f;
    //    //hose.bulgeThickness = 0.075f;
    //    handle.Return();





    //    //GetComponent<Car>().OnCarRepaired();
    //    CarIsRepaired = true;
}

