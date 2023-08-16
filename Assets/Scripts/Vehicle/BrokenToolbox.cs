using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class BrokenToolbox : MonoBehaviour
{
    [SerializeField] Transform _toolboxTarget;
    [SerializeField] GameObject _fx, _image;
    bool _hasRepaired = false;
    private InputReader _reader;

    [Inject]
    private void Init(InputReader reader)
    {
        _reader = reader;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasRepaired && other.CompareTag("Player"))
        {
            StartCoroutine(Repair());
        }
    }


    IEnumerator Repair()
    {
        Inventory inventory = GameManager.Instance.References.PlayerInventory;
        StackableItem toolbox = inventory.GetItem(Enums.StackableItemType.RepairBox);
        if (toolbox == null) { yield break; }
        inventory.RemoveItem(toolbox);

        Mover mover = FindObjectOfType<Mover>();

        Animator animator = inventory.transform.GetComponentInChildren<Animator>();
        mover.IsActive = false;
        _reader.Disable();

        yield return new WaitForSeconds(.25f);


        yield return DotweenFX(toolbox, inventory, _toolboxTarget);
        animator.Play("Crouch");
        _fx.SetActive(true);

        yield return new WaitForSeconds(1f);
        _fx.SetActive(false);
        _image.SetActive(false);
        animator.SetTrigger("Idle");

        Destroy(toolbox.gameObject);

        if (!PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
        {
            GameProgressModel tuto = FindObjectOfType<GameProgressModel>();
            tuto.OnSecondRepair();

            PlayerPrefs.SetInt(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE, 1);
        }

        //GetComponent<Car>().OnCarRepaired();
        _hasRepaired = true;
        _reader.Enable();
        mover.IsActive = true;
    }



    IEnumerator DotweenFX(StackableItem item, Inventory inventory, Transform target)
    {
        item.transform.parent = transform;
        Vector3 endPos = target.localPosition;
        Vector3 intermediatePos = new Vector3(endPos.x / 2f, endPos.y + 4f, endPos.z / 2f);
        Vector3[] path = { intermediatePos, endPos };

        item.transform.DOLocalPath(path, .35f, PathType.CatmullRom, PathMode.Full3D);
        item.transform.DOLocalRotate(target.localRotation.eulerAngles, .35f);

        yield return new WaitForSeconds(.35f);

        Sequence seq = DOTween.Sequence();
        Vector3 baseScale = item.transform.localScale;
        seq.Append(item.transform.DOScale(baseScale * 0.95f, .15f));
        seq.Append(item.transform.DOScale(target.localScale, .35f));
    }
}
