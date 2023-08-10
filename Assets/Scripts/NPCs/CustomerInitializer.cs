using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Taxi.NPC
{
    public class CustomerInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _hats;
        private void Awake()
        {
            int randInt = Random.Range(0, _hats.Length + 1);
            if (randInt < _hats.Length)
            {
                _hats[randInt].SetActive(true);
            }
        }
    }
}