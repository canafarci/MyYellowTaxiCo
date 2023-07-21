using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class WaitSpawnTrigger : MonoBehaviour
{
    private ItemSpawner _itemSpawner;
    private IWaitingEngine _payingZoneEngine;
    private void Awake()
    {
        _payingZoneEngine = GetComponent<IWaitingEngine>();
        _itemSpawner = GetComponent<ItemSpawner>();

        Assert.IsNotNull(_itemSpawner);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            Action successAction = () => _itemSpawner.SpawnItem(other);

            WaitZoneConfigSO config = new WaitZoneConfigSO(successAction);
            _payingZoneEngine.Begin(config, other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            _payingZoneEngine.Cancel(other.gameObject);
        }
    }
}
