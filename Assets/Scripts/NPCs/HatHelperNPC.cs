using System;
using System.Collections;
using System.Collections.Generic;
using Taxi.Upgrades;
using UnityEngine;
using Zenject;

namespace Taxi.NPC
{
    public class HatHelperNPC : NavMeshNPC
    {
        [Inject]
        private void Init(Vector3 spawnPos)
        {
            transform.position = spawnPos;
        }
        public class Factory : PlaceholderFactory<Vector3, HatHelperNPC>
        {

        }
    }
}

// private Inventory _inventory;
// private DriverQueue[] _queues;
// private float _hatStackersUpdateInterval = 5f;
// private Animator _animator;
// public static List<StackPickup> HatStackers;

// override protected void Awake()
// {
//     base.Awake();
//     _animator = GetComponentInChildren<Animator>();
//     _inventory = GetComponent<Inventory>();
//     _queues = FindObjectsOfType<DriverQueue>();
//     _agent.speed = UpgradesFacade.Instance.GetNPCSpeed();
//     _animator.speed = _agent.speed / 7f; //constant base speed
//     _inventory.MaxStackSize = UpgradesFacade.Instance.GetNPCInventorySize();
// }
// private void OnEnable()
// {
//     UpgradesFacade.Instance.OnNPCInventorySizeUpgrade += IncreaseInventoryUpgradeHandler;
//     UpgradesFacade.Instance.OnNPCSpeedUpgrade += IncreaseSpeedUpgradeHandler;
// }
// private void OnDisable()
// {
//     UpgradesFacade.Instance.OnNPCInventorySizeUpgrade -= IncreaseInventoryUpgradeHandler;
//     UpgradesFacade.Instance.OnNPCSpeedUpgrade -= IncreaseSpeedUpgradeHandler;
// }
// private void Start()
// {
//     StartCoroutine(NPCLoop());

//     // Only start the update coroutine in one NPC instance
//     if (HatStackers == null)
//     {
//         HatStackers = new List<StackPickup>(FindObjectsOfType<StackPickup>());
//         StartCoroutine(UpdateHatStackersPeriodically());
//     }
// }
// private void IncreaseSpeedUpgradeHandler(float speed)
// {
//     _agent.speed = speed;
//     _animator.speed = _agent.speed / 7f; //constant base speed
// }
// private void IncreaseInventoryUpgradeHandler(int size)
// {
//     _inventory.MaxStackSize = size;
// }
// private IEnumerator UpdateHatStackersPeriodically()
// {
//     while (true)
//     {
//         yield return new WaitForSeconds(1f);
//         HatStackers = new List<StackPickup>(FindObjectsOfType<StackPickup>());
//         yield return new WaitForSeconds(_hatStackersUpdateInterval);
//     }
// }
// private IEnumerator NPCLoop()
// {
//     while (true)
//     {
//         yield return new WaitForSeconds(.25f);

//         // Check if there is an available driver without a hat
//         Driver driver = null;
//         DriverQueue queue = null;
//         foreach (DriverQueue q in _queues)
//         {
//             driver = q.GetDriverWithoutHat();
//             queue = q;
//             if (driver != null)
//                 break;
//         }
//         // Continue waiting if there is no driver
//         if (driver == null)
//             continue;

//         // Get the type of hat the driver has
//         Enums.StackableItemType ItemType = driver.Hat;

//         // Get the corresponding hat stacker
//         StackPickup hatStacker = null;
//         foreach (StackPickup stacker in HatStackers)
//         {
//             if (stacker.IsHatStacker)
//             {
//                 hatStacker = stacker;
//                 break;
//             }
//         }
//         if (hatStacker == null) continue;

//         // Get to the hat stacker position and wait until the hat is obtained
//         yield return GetToPos(hatStacker.transform.position);
//         while (_inventory.ItemCount < 0)
//             yield return new WaitForSeconds(.5f);

//         // Give the hat to the driver
//         yield return GetToPos(queue.transform.position);

//         // Wait until the hat is given
//         while (_inventory.ItemCount > 1)
//             yield return new WaitForSeconds(.5f);
//     }
// }