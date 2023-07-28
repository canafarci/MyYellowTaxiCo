using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Taxi.NPC
{
    public class HatDistributor : MonoBehaviour
    {
        private List<QueueSpot> _queueSpots = new List<QueueSpot>();
        private Stacker _stacker;

        private void Awake()
        {
            _stacker = GetComponent<Stacker>();
        }

        private void Start()
        {
            QueueSpot.OnNewQueueSpotActivated += HandleNewQueueSpotActivated;
            StartCoroutine(TryGiveHatLoop());
        }

        private void OnDestroy()
        {
            QueueSpot.OnNewQueueSpotActivated -= HandleNewQueueSpotActivated;
        }

        private void HandleNewQueueSpotActivated(object sender, OnNewQueueSpotActivatedEventArgs e)
        {
            QueueSpot queueSpot = sender as QueueSpot;
            Assert.IsNotNull(queueSpot, "Sender should be a valid QueueSpot.");

            _queueSpots.Add(queueSpot);
        }

        private IEnumerator TryGiveHatLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);

                if (_stacker.ItemStack.TryPop(out StackableItem item))
                {
                    DistributeHatToDrivers(item);
                }
            }
        }

        private void DistributeHatToDrivers(StackableItem hat)
        {
            foreach (QueueSpot spot in _queueSpots)
            {
                if (spot.TryGetDriver(out Driver driver) && !driver.DriverHasHat())
                {
                    driver.GiveHat(hat);
                }
            }
        }
    }
}
