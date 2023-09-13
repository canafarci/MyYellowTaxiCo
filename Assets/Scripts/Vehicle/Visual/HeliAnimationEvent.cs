using UnityEngine;

namespace TaxiGame.Vehicles.Visuals
{
    public class HeliAnimationEvent : MonoBehaviour
    {
        [SerializeField] GameObject _npc;
        public void DisableNPC()
        {
            _npc.SetActive(false);
        }
    }
}

