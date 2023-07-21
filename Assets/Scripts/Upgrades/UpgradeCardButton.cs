using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardButton : MonoBehaviour
{
    [SerializeField] Enums.UpgradeType _upgradeType;
    private UpgradeCardVisual _upgradeVisual;
    private void Awake()
    {
        _upgradeVisual = GetComponent<UpgradeCardVisual>();
    }
    public void OnButtonClicked()
    {
        //buy the upgrade
        IUpgradeCommand upgradeCommand = UpgradeClient.Instance.GetUpgradeCommand(_upgradeType, _upgradeVisual);
        UpgradeInvoker.Instance.InvokeUpgradeCommand(upgradeCommand);

        //check next upgrade can be bought
        CheckCanBuyUpgrade();
    }

    private void OnEnable()
    {
        GameManager.Instance.Resources.MoneyChangeHandler += CheckCanBuy;
        CheckCanBuyUpgrade();
    }
    private void CheckCanBuyUpgrade()
    {
        //check next upgrade can be bought
        IUpgradeCommand checkCommand = UpgradeClient.Instance.GetCheckCommand(_upgradeType, _upgradeVisual);
        UpgradeInvoker.Instance.InvokeUpgradeCommand(checkCommand);
    }
    private void CheckCanBuy(float obj)
    {
        CheckCanBuyUpgrade();
    }

    //cleanup
    private void OnDisable()
    {
        GameManager.Instance.Resources.MoneyChangeHandler -= CheckCanBuy;
    }

}
