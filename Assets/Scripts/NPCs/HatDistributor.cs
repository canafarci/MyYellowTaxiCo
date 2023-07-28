using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taxi.NPC
{
    public class HatDistributor : MonoBehaviour
    {
        private Stacker _stacker;
        private DriverQueue _driverQueue;
        private void Awake()
        {
            _stacker = GetComponent<Stacker>();
            _driverQueue = GetComponentInParent<DriverQueue>();
        }
        private void Start()
        {
            StartCoroutine(TryGiveHatLoop());
        }
        private IEnumerator TryGiveHatLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                List<Driver> drivers = _driverQueue.GetDrivers();
                if (_stacker.ItemStack.Count > 0 && drivers.Count > 0)
                {
                    TryDistributeHatToDrivers(drivers);
                }
            }
        }
        private void TryDistributeHatToDrivers(List<Driver> drivers)
        {

            foreach (Driver driver in drivers)
            {
                if (!driver.DriverHasHat())
                {
                    if (_stacker.ItemStack.TryPop(out StackableItem hat))
                        driver.GiveHat(hat);
                    else
                        break;
                }
            }
        }
    }
}
