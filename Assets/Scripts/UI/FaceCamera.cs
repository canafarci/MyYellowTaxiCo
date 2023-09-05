using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.UI
{
    public class FaceCamera : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.LookAt(transform.position - Camera.main.transform.position);
        }
    }
}
