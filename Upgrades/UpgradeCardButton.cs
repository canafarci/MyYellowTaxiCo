using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardButton : MonoBehaviour
{
    [SerializeField] Enums.UpgradeType _upgradeType;
    [SerializeField] GameObject[] _upgradeDots;
    [SerializeField] UpgradeData _upgradeData;
    [SerializeField] TextMeshProUGUI _costText;
    Upgrader _upgrader;
    Button _button;
    string _upgradeName;
    int _currentIndex;
    float _cost;
    private void Awake()
    {
        SetTypeName();
        _upgrader = FindObjectOfType<Upgrader>();
        _button = GetComponentInChildren<Button>();
    }
    private void OnEnable()
    {
        _currentIndex = PlayerPrefs.GetInt(_upgradeName);
        ActivateDots();
        //check if upgrade can be bought
        CheckCanBuy(GameManager.Instance.Resources.Money);
        GameManager.Instance.Resources.MoneyChangeHandler += CheckCanBuy;
    }

    private void OnDisable()
    {
        GameManager.Instance.Resources.MoneyChangeHandler -= CheckCanBuy;
    }


    public void OnButtonClicked()
    {
        if (IndexAtMaxLength()) return;

        _currentIndex++;
        _upgrader.Upgrade(_upgradeType, _currentIndex);
        GameManager.Instance.Resources.OnPayMoney(_cost);
        ActivateDots();

        CheckCanBuy(GameManager.Instance.Resources.Money);
    }

    private void CheckCanBuy(float currentMoney)
    {
        if (IndexAtMaxLength()) return;

        if (_upgradeType == Enums.UpgradeType.PlayerSpeed)
        {
            _cost = _upgradeData.SpeedModifiers[_currentIndex + 1].Cost;
        }
        else if (_upgradeType == Enums.UpgradeType.PlayerIncome)
        {
            _cost = _upgradeData.IncomeModifiers[_currentIndex + 1].Cost;
        }
        else if (_upgradeType == Enums.UpgradeType.PlayerInventorySize)
        {
            _cost = _upgradeData.PlayerInventorySizes[_currentIndex + 1].Cost;
        }
        else if (_upgradeType == Enums.UpgradeType.HelperNPCCount)
        {
            _cost = _upgradeData.HelperNPCCount[_currentIndex + 1].Cost;
        }
        else if (_upgradeType == Enums.UpgradeType.HelperNPCInventorySize)
        {
            _cost = _upgradeData.HelperNPCInventorySizes[_currentIndex + 1].Cost;
        }
        else if (_upgradeType == Enums.UpgradeType.HelperNPCSpeed)
        {
            _cost = _upgradeData.HelperNPCSpeeds[_currentIndex + 1].Cost;
        }

        CheckHasMoney(currentMoney);
        SetCostText(_cost);
    }

    private void CheckHasMoney(float currentMoney)
    {
        if (_cost > currentMoney)
            _button.interactable = false;
        else
            _button.interactable = true;
    }
    void SetCostText(float cost)
    {
        if (cost > 0f)
        {
            if (cost >= 1000)
            {
                if (cost % 1000 == 0)
                    _costText.text = (cost / 1000).ToString("F0") + "K";
                else
                    _costText.text = (cost / 1000).ToString("F1") + "K";
            }
            else
            {
                _costText.text = cost.ToString("F0");
            }
        }
        else
            _costText.text = "FREE";
    }
    private void ActivateDots()
    {
        for (int i = 0; i < _currentIndex; i++)
            _upgradeDots[i].SetActive(true);
    }
    private void SetTypeName()
    {
        if (_upgradeType == Enums.UpgradeType.PlayerSpeed)
            _upgradeName = Globals.PLAYER_SPEED_KEY;
        else if (_upgradeType == Enums.UpgradeType.PlayerIncome)
            _upgradeName = Globals.PLAYER_INCOME_KEY;
        else if (_upgradeType == Enums.UpgradeType.PlayerInventorySize)
            _upgradeName = Globals.PLAYER_INVENTORY_KEY;
        else if (_upgradeType == Enums.UpgradeType.HelperNPCCount)
            _upgradeName = Globals.NPC_COUNT_KEY;
        else if (_upgradeType == Enums.UpgradeType.HelperNPCInventorySize)
            _upgradeName = Globals.NPC_INVENTORY_KEY;
        else
            _upgradeName = Globals.NPC_SPEED_KEY;
    }

    bool IndexAtMaxLength()
    {
        bool isAtMax;
        if (_upgradeType == Enums.UpgradeType.HelperNPCCount)
            isAtMax = _currentIndex >= _upgradeData.HelperNPCCount.Length - 1;
        else
        {
            //return  true if index is at the end of upgrades, reference list is arbitary, all of them has the same length
            isAtMax = _currentIndex >= _upgradeData.SpeedModifiers.Length - 1;
        }

        if (isAtMax)
        {
            _costText.text = "MAX";
            _button.interactable = false;
        }
        return isAtMax;
    }
}
