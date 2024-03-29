using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using UnityEngine;

public class DotweenFX : MonoBehaviour
{
    // Pool size and money pool list
    public int _poolSize = 20;
    private List<GameObject> _moneyPool;

    // Singleton instance for DotweenFX
    public static DotweenFX Instance { get; private set; }

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public static IEnumerator StackTweenItem(StackableItem item, Vector3 endPos)
    {
        item.transform.DOKill();
        Vector3 startPos = item.transform.position;
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 1f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };
        item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);

        float targetRotation = Random.Range(0f, 360f);
        item.transform.DOLocalRotate(new Vector3(0, targetRotation, 0f), 0.5f);

        yield return new WaitForSeconds(0.51f);

        Sequence seq = DOTween.Sequence();
        Vector3 endBaseScale = item.transform.localScale;
        seq.Append(item.transform.DOScale(endBaseScale * 1.2f, .1f));
        seq.Append(item.transform.DOScale(endBaseScale, .1f));
    }


    public static IEnumerator WearHatTween(StackableItem item, Transform hatTransform, ParticleSystem fx)
    {
        item.transform.DOKill();
        item.transform.parent = hatTransform;

        Vector3 endPos = hatTransform.localPosition;
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 1f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };
        item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);

        item.transform.DOLocalRotate(hatTransform.localRotation.eulerAngles, 0.5f);
        fx.Play();
        yield return new WaitForSeconds(0.6f);

        item.transform.DOKill();
        item.transform.DOScale(1f, .2f);

        Sequence seq1 = DOTween.Sequence();
        Vector3 baseScale1 = new Vector3(Globals.HAT_WORN_SCALE, Globals.HAT_WORN_SCALE, Globals.HAT_WORN_SCALE);
        seq1.Append(item.transform.GetChild(0).DOScale(baseScale1 * 1.2f, .1f));
        seq1.Append(item.transform.GetChild(0).DOScale(baseScale1, .1f));
    }

    public static IEnumerator RepairTireTween(StackableItem item, Transform target)
    {
        item.transform.DOKill();
        item.transform.parent = target.parent;
        Vector3 endPos = target.localPosition;
        Vector3 startPos = item.transform.position;
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 1f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };

        item.transform.DOLocalPath(path, .5f, PathType.CatmullRom, PathMode.Full3D);
        item.transform.DOLocalRotate(target.localRotation.eulerAngles, .5f);

        yield return new WaitForSeconds(0.51f);

        Sequence seq = DOTween.Sequence();
        Vector3 endBaseScale = item.transform.localScale;
        seq.Append(item.transform.DOScale(endBaseScale * 1.2f, .1f));
        seq.onComplete = () => item.gameObject.SetActive(false);
    }


}
