using System.Linq;
using TaxiGame.NPC;
using UnityEngine;
using UnityEngine.Events;

public class GameProgressModel : MonoBehaviour
{
    [SerializeField]
    UnityEvent _firstCarReturnWithoutCharger, _onFirstCharge, _afterFirstChargeReload,
                _firstReturnBroken, _onFirstRepair, _afterRepairLoad,
                _firstReturnTire, _onFirstTireChange, _afterTireReload,
                _onFirstCustomerDelivered, _afterCustomerDeliveredLoad,
                _onFirstWandererSpawn, _wandererTriggered, _afterWandererDeliveredLoad;

    [SerializeField] GameObject[] _tutorialTexts;

    private void Start()
    {
        Invoke(nameof(Load), .3f);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(Globals.FIRST_CHARGER_TUTORIAL_COMPLETE))
        {
            _afterFirstChargeReload.Invoke();
        }
        if (PlayerPrefs.HasKey(Globals.SECOND_BROKEN_TUTORIAL_COMPLETE))
        {
            _afterRepairLoad.Invoke();
        }
        if (PlayerPrefs.HasKey(Globals.THIRD_TIRE_TUTORIAL_COMPLETE))
        {
            _afterTireReload.Invoke();
        }
        if (PlayerPrefs.HasKey(Globals.FOURTH_CUSTOMER_TUTORIAL_COMPLETE))
        {
            _afterCustomerDeliveredLoad.Invoke();
        }
        if (PlayerPrefs.HasKey(Globals.FIFTH_VIP_TUTORIAL_COMPLETE))
        {
            _afterWandererDeliveredLoad.Invoke();
        }
    }

    public void FirstReturnCarWithoutCharger()
    {
        _firstCarReturnWithoutCharger.Invoke();
    }
    public void SecondReturnBroken()
    {
        _firstReturnBroken.Invoke();
    }
    public void ThirdReturnBroken()
    {
        _firstReturnTire.Invoke();
    }

    public void OnFirstCharge()
    {
        _onFirstCharge.Invoke();

        // var data = new Ketchapp.MayoSDK.Analytics.Data();
        // data.AddValue("ProgressionStatus", "Started");
        // data.AddValue("Money", (int)ResourceTracker.Instance.PlayerMoney);
        // KetchappSDK.Analytics.CustomEvent("----SecondHatTutorialStart", data);
    }
    public void OnSecondRepair()
    {
        _onFirstRepair.Invoke();
    }
    public void OnThirdRepair()
    {
        _onFirstTireChange.Invoke();
    }
    public void DisableAllChildren()
    {
        _tutorialTexts.ToList().ForEach(x => x.SetActive(false));
    }
    public void OnFirstCustomerDelivered()
    {
        _onFirstCustomerDelivered.Invoke();
    }
    public void VIPTriggered()
    {
        _wandererTriggered.Invoke();
    }

    public void OnFirstWandererSpawned(Wanderer wanderer)
    {
        _onFirstWandererSpawn.Invoke();
        FindObjectOfType<ObjectiveArrow>().ChangeObjective(wanderer.transform); //TODO DI
    }
}