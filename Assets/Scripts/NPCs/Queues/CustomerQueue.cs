using UnityEngine;

namespace TaxiGame.NPC
{
    public class CustomerQueue : MonoBehaviour, INPCQueue
    {
        [SerializeField] private CustomerQueueSpot _headNode;
        [SerializeField] private CustomerQueueSpot _endNode;

        public void AddToQueue(RiderNPC npc)
        {
            _headNode.MoveFollower(npc);
        }

        public bool QueueIsFull()
        {
            return !_headNode.IsEmpty();
        }

        public bool TryGetCustomer(out Customer customer)
        {
            customer = _endNode.GetNPC() as Customer;

            if (customer == null || !customer.IsCustomerSeated())
            {
                return false;
            }
            else
            {
                _endNode.Clear();
                _endNode.ShiftQueue();
                return true;
            }
        }
    }

}