using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taxi.NPC
{
    public class NPCCosmetics : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hatsSuit, _hatsTshirt, _characters;

        private void Awake()
        {
            //TODO factory
            // int randInt = Random.Range(0, _characters.Length);
            // _characters[randInt].SetActive(true);
            // GetComponent<NavMeshAnimator>().Animator = GetComponentInChildren<Animator>();
            // GameObject[] hats = randInt == 0 ? _hatsSuit : _hatsTshirt;

            // randInt = Random.Range(0, hats.Length + 1);
            // if (randInt == hats.Length) { return; }
            // hats[randInt].SetActive(true);
        }
    }
}