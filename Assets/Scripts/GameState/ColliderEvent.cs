using UnityEngine;
using UnityEngine.Events;

namespace TaxiGame.GameState
{
    public class ColliderEvent : MonoBehaviour
    {
        [SerializeField] string _tagName;
        [SerializeField] UnityEvent _onTriggerEnterEvent, _onTriggerExitEvent, _onTriggerStayEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_tagName))
                _onTriggerEnterEvent.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_tagName))
                _onTriggerExitEvent.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(_tagName))
                _onTriggerStayEvent.Invoke();
        }
    }
}
