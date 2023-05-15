using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BrokenTire : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] GameObject _image;
    bool _hasRepaired = false;
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
        StackableItem item = inventory.GetItem(Enums.StackableItemType.Tire);
        if (item == null) { yield break; }
        inventory.RemoveItem(item);

        Animator animator = inventory.transform.GetComponentInChildren<Animator>();
        Mover mover = FindObjectOfType<Mover>();
        InputReader reader = FindObjectOfType<InputReader>();
        mover.IsActive = false;
        reader.Disable();

        item.transform.parent = transform;

        yield return StartCoroutine(DotweenFX.RepairTireTween(item, _target));


        yield return new WaitForSeconds(1f);

        _image.SetActive(false);

        if (!PlayerPrefs.HasKey(StaticVariables.THIRD_TIRE_TUTORIAL_COMPLETE))
        {
            ConditionalTutorial tuto = FindObjectOfType<ConditionalTutorial>();
            tuto.OnThirdRepair();

            PlayerPrefs.SetInt(StaticVariables.THIRD_TIRE_TUTORIAL_COMPLETE, 1);
        }

        GetComponent<Car>().OnCarRepaired();
        _hasRepaired = true;
        mover.IsActive = true;
        reader.Enable();
    }

}
