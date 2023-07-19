public interface IStackedItemSpawner
{
    public bool CanSpawnItem();
    public void SpawnItem();
    public void SetSpawnRate(float rate);
}