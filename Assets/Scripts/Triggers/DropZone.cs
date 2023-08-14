using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Linq;
using TaxiGame.NPC;

public class DropZone : MonoBehaviour
{
    // public bool CarIsReady { get { return _spawner.Car != null; } }
    // public bool DriverOnWay = false;
    // public Enums.StackableItemType HatType;
    // Spawner _spawner;
    // Coroutine _unloadCoroutine;
    // DriverQueue _driverQueue;

    // private void Awake()
    // {
    //     _spawner = GetComponent<Spawner>();
    //     _driverQueue = FindObjectsOfType<DriverQueue>().Where(x => x.HatType == HatType).LastOrDefault();
    //     _spawner.OnMovedAway = () => DriverOnWay = false;
    // }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //         _unloadCoroutine = StartCoroutine(UnloadLoop());
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //         StopCoroutine(_unloadCoroutine);
    // }

    // IEnumerator UnloadLoop()
    // {
    //     Inventory inventory = GameManager.Instance.References.PlayerInventory;

    //     for (int i = 0; i < Mathf.Infinity; i++)
    //     {
    //         Car car = _spawner.Car;
    //         NavMeshNPC[] followers = inventory.GetFollowers(HatType);

    //         if (car != null && followers.Length != 0 && DriverOnWay == false)
    //         {
    //             yield return StartCoroutine(Unload(followers, car));
    //             continue;
    //         }

    //         yield return new WaitForSeconds(.25f);
    //     }
    // }
    // //called from the queue by default
    // public void CallMoveDriver(Driver driver) => StartCoroutine(MoveDriver(driver, false));

    // //default bool is set when it is called by passengers
    // IEnumerator MoveDriver(Driver driver, bool withPassengers = true)
    // {
    //     Car car = _spawner.Car;
    //     yield return StartCoroutine(driver.OpenDoorAndGetIn(car.transform.position));
    //     car.PreMoveFX(withPassengers);
    //     yield return new WaitForSeconds(1f);
    //     _spawner.StartMove();
    // }
    // TODO case when followers dropped to the car zone
    // IEnumerator Unload(NavMeshNPC[] followers, Car car)
    // {
    //     if (!PlayerPrefs.HasKey(Globals.FOURTH_CUSTOMER_TUTORIAL_COMPLETE))
    //     {
    //         FindObjectOfType<ConditionalTutorial>().OnFirstCustomerDelivered();
    //         PlayerPrefs.SetInt(Globals.FOURTH_CUSTOMER_TUTORIAL_COMPLETE, 1);
    //     }

    //     for (int i = 0; i < followers.Length; i++)
    //         StartCoroutine(followers[i].OpenDoorAndGetIn(car.transform.position));

    //     Driver carDriver = null;
    //     DriverOnWay = true;

    //     yield return (StartCoroutine(_driverQueue.CallDriver((driver) => carDriver = driver)));
    //     yield return StartCoroutine(MoveDriver(carDriver));
    // }
}