using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TaxiGame.Items;
using UnityEngine;
using Zenject;

public class BrokenTire : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] GameObject _image;
    bool _hasRepaired = false;
    private InputReader _reader;
    private void OnTriggerEnter(Collider other)
    {
        if (!_hasRepaired && other.CompareTag("Player"))
        {
            StartCoroutine(Repair());
        }
    }

    [Inject]
    private void Init(InputReader reader)
    {
        _reader = reader;
    }

    IEnumerator Repair()
    {
        Inventory inventory = GameManager.Instance.References.PlayerInventory;
        StackableItem item = inventory.PopInventoryObject(InventoryObjectType.Tire) as StackableItem;
        if (item == null) { yield break; }

        Animator animator = inventory.transform.GetComponentInChildren<Animator>();
        Mover mover = FindObjectOfType<Mover>();
        mover.IsActive = false;
        _reader.Disable();

        item.transform.parent = transform;

        yield return StartCoroutine(DotweenFX.RepairTireTween(item, _target));


        yield return new WaitForSeconds(1f);

        _image.SetActive(false);

        if (!PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
        {
            GameProgressModel tuto = FindObjectOfType<GameProgressModel>();
            tuto.OnThirdRepair();

            PlayerPrefs.SetInt(Globals.THIRD_TIRE_TUTORIAL_COMPLETE, 1);
        }

        //GetComponent<Car>().OnCarRepaired();
        _hasRepaired = true;
        mover.IsActive = true;
        _reader.Enable();
    }

}
