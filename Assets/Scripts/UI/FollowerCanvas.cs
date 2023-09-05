using System.Collections;
using UnityEngine;

namespace TaxiGame.UI
{
    public class FollowerCanvas : MonoBehaviour
    {
        [SerializeField] private bool _isWanderer;
        private void Start()
        {
            if (!_isWanderer)
            {
                StartCoroutine(Loop());
            }
        }

        private IEnumerator Loop()
        {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                SwitchActivation(false);
                yield return new WaitForSeconds(6f);
                SwitchActivation(true);
            }
        }

        private void SwitchActivation(bool enable)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(enable);
            }
        }
    }
}