using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.NPC
{
    public class WandererTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) { return; }

            Wanderer wanderer = GetComponent<Wanderer>();
            //wanderer.FollowPlayer(GameManager.Instance.References.PlayerInventory);
            GetComponent<Collider>().enabled = false;
        }
    }
}