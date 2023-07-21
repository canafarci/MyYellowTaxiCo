using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatHelperNPC : NavMeshNPC
{
    Inventory _inventory;
    DriverQueue[] _queues;
    public static List<StackPickup> _hatStackers;
    float _hatStackersUpdateInterval = 5f;
    Animator _animator;

    override protected void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
        _inventory = GetComponent<Inventory>();
        _queues = FindObjectsOfType<DriverQueue>();
        _agent.speed = Upgrades.Instance.NPCSpeed;
        _animator.speed = _agent.speed / 7f; //constant base speed
        _inventory.MaxStackSize = Upgrades.Instance.NPCInventorySize;
    }
    private void OnEnable()
    {
        Upgrades.Instance.OnNPCInventorySizeUpgrade += IncreaseInventoryHandler;
        Upgrades.Instance.OnNPCSpeedUpgrade += IncreaseSpeedHandler;
    }
    private void OnDisable()
    {
        Upgrades.Instance.OnNPCInventorySizeUpgrade -= IncreaseInventoryHandler;
        Upgrades.Instance.OnNPCSpeedUpgrade -= IncreaseSpeedHandler;
    }
    private void IncreaseSpeedHandler(float speed)
    {
        _agent.speed = speed;
        _animator.speed = _agent.speed / 7f; //constant base speed
    }
    private void IncreaseInventoryHandler(int size)
    {
        _inventory.MaxStackSize = size;
    }

    private void Start()
    {
        StartCoroutine(NPCLoop());

        // Only start the update coroutine in one NPC instance
        if (_hatStackers == null)
        {
            _hatStackers = new List<StackPickup>(FindObjectsOfType<StackPickup>());
            StartCoroutine(UpdateHatStackersPeriodically());
        }
    }

    void IncreaseSize(int size)
    {
        _inventory.MaxStackSize = size;
    }

    IEnumerator UpdateHatStackersPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _hatStackers = new List<StackPickup>(FindObjectsOfType<StackPickup>());
            yield return new WaitForSeconds(_hatStackersUpdateInterval);
        }
    }

    IEnumerator NPCLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);

            // Check if there is an available driver without a hat
            Driver driver = null;
            DriverQueue queue = null;
            foreach (DriverQueue q in _queues)
            {
                driver = q.GetDriverWithoutHat();
                queue = q;
                if (driver != null)
                    break;
            }
            // Continue waiting if there is no driver
            if (driver == null)
                continue;

            // Get the type of hat the driver has
            Enums.StackableItemType ItemType = driver.Hat;

            // Get the corresponding hat stacker
            StackPickup hatStacker = null;
            foreach (StackPickup stacker in _hatStackers)
            {
                if (stacker.IsHatStacker)
                {
                    hatStacker = stacker;
                    break;
                }
            }
            if (hatStacker == null) continue;

            // Get to the hat stacker position and wait until the hat is obtained
            yield return GetToPos(hatStacker.transform.position);
            while (_inventory.ItemCount < 0)
                yield return new WaitForSeconds(.5f);

            // Give the hat to the driver
            yield return GetToPos(queue.transform.position);

            // Wait until the hat is given
            while (_inventory.ItemCount > 1)
                yield return new WaitForSeconds(.5f);
        }
    }
}
