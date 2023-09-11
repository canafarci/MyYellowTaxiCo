using TaxiGame.GameState.Unlocking;
using UnityEngine;
using Zenject;

namespace TaxiGame.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject _stackableItem;
        [SerializeField] protected Transform _startTransform;
        private IUnlockable _unlockable;

        //initialization
        [Inject]
        private void Init([InjectOptional] IUnlockable unlockable)
        {
            _unlockable = unlockable;
        }

        public void SpawnItem(Collider other)
        {
            if (CanSpawnItem(other))
            {
                StackableItem item = GameObject.Instantiate(_stackableItem, _startTransform.position, _startTransform.rotation).GetComponent<StackableItem>();
                Inventory inventory = other.GetComponent<Inventory>();
                inventory.AddObjectToInventory(item);

                _unlockable?.UnlockObject();
            }
        }
        private bool CanSpawnItem(Collider other)
        {
            Inventory inventory = other.GetComponent<Inventory>();
            return inventory.GetStackableItemCountInInventory() == 0;
        }
    }
}


