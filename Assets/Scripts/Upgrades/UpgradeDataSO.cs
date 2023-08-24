using UnityEngine;
namespace TaxiGame.Upgrades
{
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "Config/Upgrade Data", order = 0)]
    public class UpgradeDataSO : ScriptableObject
    {
        public SpeedModifier[] SpeedModifiers;
        public IncomeModifier[] IncomeModifiers;
        public PlayerInventorySize[] PlayerInventorySizes;
        public HelperNPCCount[] HelperNPCCount;
        public HelperNPCSpeed[] HelperNPCSpeeds;
        public HelperNPCInventorySize[] HelperNPCInventorySizes;
        public StackSpeed[] StackSpeeds;
        public int BaseMoveSpeed = 5;
    }

    [System.Serializable]
    public struct StackSpeed
    {
        public float SpawnRate;
        public float Cost;
    }
    [System.Serializable]
    public struct SpeedModifier
    {
        public float SpeedMultiplier;
        public float Cost;
    }

    [System.Serializable]
    public struct IncomeModifier
    {
        public float IncomeMultiplier;
        public float Cost;
    }

    [System.Serializable]
    public struct PlayerInventorySize
    {
        public int Size;
        public float Cost;
    }
    [System.Serializable]
    public struct HelperNPCCount
    {
        public float Cost;
    }
    [System.Serializable]
    public struct HelperNPCSpeed
    {
        public float Speed;
        public float Cost;
    }
    [System.Serializable]
    public struct HelperNPCInventorySize
    {
        public int Size;
        public float Cost;
    }
}