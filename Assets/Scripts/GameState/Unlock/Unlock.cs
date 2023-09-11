namespace TaxiGame.GameState.Unlocking
{
    public class Unlock : UnlockBase
    {
        private void Start()
        {
            if (HasUnlockedBefore())
                UnlockObject();
            else
                SendAnalyticsDataForProgressionStart();
        }
    }
}