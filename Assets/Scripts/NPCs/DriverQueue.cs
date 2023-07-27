using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class DriverQueue : MonoBehaviour
{
    public Enums.StackableItemType ItemType;
    [SerializeField] private Transform[] _driverSpots;
    private List<DriverInQueue> _drivers = new List<DriverInQueue>();
    private List<SitSpot> _spots = new List<SitSpot>();
    private Coroutine _dropLoop;
    private Inventory _inventory;
    private Stacker _stacker;
    private bool _npcWaitingForDriver = false;
    private void Awake()
    {
        _stacker = GetComponentInChildren<Stacker>();
        StartCoroutine(TryGiveHatToDriver());

        for (int i = 0; i < _driverSpots.Length; i++)
        {
            _spots.Add(new SitSpot(_driverSpots[i], false));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            _inventory = other.GetComponent<Inventory>();
            _dropLoop = StartCoroutine(DropLoop());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("HatHelperNPC"))
        {
            if (_dropLoop != null)
                StopCoroutine(_dropLoop);
        }
    }
    public Driver GetDriverWithoutHat()
    {
        DriverInQueue driverIQ = _drivers.Where(x => x.HasHat == false).FirstOrDefault();
        if (driverIQ != null)
            return driverIQ.Driver;
        return null;
    }
    public void GetIntoQueue(Driver driver) => StartCoroutine(AddToList(driver));
    public IEnumerator CallDriver(Action<Driver> callback)
    {
        _npcWaitingForDriver = true;
        yield return StartCoroutine(MoveDriver((driver) =>
        {
            driver.ActivateHat();
            driver.GetComponentInChildren<Animator>().SetBool("IsSitting", false);
            callback(driver);
            _npcWaitingForDriver = false;
        }));
    }
    private IEnumerator AddToList(Driver driver)
    {
        SitSpot spot = _spots.Where(x => x.IsOccupied != true).OrderBy(x => Vector3.Distance(x.Spot.position, driver.transform.position)).FirstOrDefault();
        spot.IsOccupied = true;
        DriverInQueue qSpot = new DriverInQueue(driver, spot);
        driver.DriverInQueue = qSpot;
        StartCoroutine(driver.GetToPosAndSit(qSpot.SitSpot.Spot));
        yield return new WaitForSeconds(0.25f);
        _drivers.Add(qSpot);
    }
    private IEnumerator DropLoop()
    {
        while (_inventory.GetItem(ItemType))
        {
            StackableItem item = _inventory.GetItem(ItemType);
            _inventory.RemoveItem(item);
            _stacker.StackItem(item);
            yield return new WaitForSeconds(.25f);
        }
    }
    private IEnumerator TryGiveHatToDriver()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);

            if (_drivers.Count < 1) { continue; }
            int index = 0;

            while (true && !_npcWaitingForDriver)
            {
                DriverInQueue currentSpot = _drivers[index];
                Driver driver = currentSpot.Driver;
                StackableItem item;
                if (!currentSpot.HasHat && _stacker.ItemStack.TryPop(out item))
                {
                    StartCoroutine(driver.GetHatAndGoToCar(item, () => MoveQueue(driver.DriverInQueue)));
                    _drivers.Remove(currentSpot);
                    break;
                }
                else if (index == _drivers.Count - 1) { break; }
                else
                    index += 1;
            }
        }
    }
    private void MoveQueue(DriverInQueue diq)
    {
        SitSpot spot = diq.SitSpot;

        spot.IsOccupied = false;

        _drivers.Remove(diq);
    }
    private IEnumerator MoveDriver(Action<Driver> callback)
    {
        while (true)
        {
            if (_drivers.Count > 0)
            {
                DriverInQueue spot = _drivers[0];
                Driver driver = spot.Driver;
                callback(driver);
                MoveQueue(driver.DriverInQueue);
                break;
            }
            else
                yield return new WaitForSeconds(1f);
        }
    }
}

public class SitSpot
{
    public SitSpot(Transform spot, bool isOccupied)
    {
        IsOccupied = isOccupied;
        Spot = spot;
    }
    public bool IsOccupied;
    public Transform Spot;
}
public class DriverInQueue
{
    public DriverInQueue(Driver driver, SitSpot spot, bool hasHat = false)
    {
        Driver = driver;
        SitSpot = spot;
        HasHat = hasHat;
    }
    public Driver Driver;
    public SitSpot SitSpot;
    public bool HasHat;
}
