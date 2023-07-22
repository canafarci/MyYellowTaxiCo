using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Taxi.Upgrades
{
    public class UpgradeClient : MonoBehaviour
    {
        [SerializeField] private UpgradeDataSO _upgradeData;
        public static UpgradeClient Instance;
        private void Awake()
        {
            if (Instance)
                Destroy(gameObject);
            else
            {
                Instance = this;
            }
        }
        private void Start()
        {
            InitializeUpgradeCards();
        }

        private void InitializeUpgradeCards()
        {
            UpgradeCardButton[] upgrades = FindObjectsOfType<UpgradeCardButton>(true);

            foreach (UpgradeCardButton upgrade in upgrades)
            {
                UpgradeCardVisual visual = upgrade.GetComponent<UpgradeCardVisual>();

                SetUpUpgradeCommand(upgrade, visual);
                SetUpCheckCommand(upgrade, visual);
            }
        }

        private void SetUpCheckCommand(UpgradeCardButton upgrade, UpgradeCardVisual visual)
        {
            IUpgradeCommand checkCommand = GetCheckCommand(upgrade.GetUpgradeType(), visual);
            upgrade.SetCheckCanUpgradeCommand(checkCommand);
        }

        private void SetUpUpgradeCommand(UpgradeCardButton upgrade, UpgradeCardVisual visual)
        {
            IUpgradeCommand upgradeCommand = GetUpgradeCommand(upgrade.GetUpgradeType(), visual);
            upgrade.SetUpgradeCommand(upgradeCommand);
        }

        private IUpgradeCommand GetUpgradeCommand(Enums.UpgradeType upgradeType, UpgradeCardVisual visual)
        {
            return new ButtonUpgradeCommand(visual, upgradeType);
        }
        private IUpgradeCommand GetCheckCommand(Enums.UpgradeType upgradeType, UpgradeCardVisual visual)
        {
            return new CheckCanUpgradeCommand(visual, upgradeType);
        }
        public IUpgradeCommand GetLoadUpgradeCommand(Enums.UpgradeType upgradeType)
        {
            return new LoadUpgradeCommand(upgradeType);
        }
    }
}