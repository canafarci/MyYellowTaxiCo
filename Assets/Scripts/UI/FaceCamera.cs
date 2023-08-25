using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private readonly Vector3 _lookOffset = Globals.CAMERA_ROTATION_OFFSET + new Vector3(0f, -35f, 0f);
    private void Update()
    {
        transform.LookAt(transform.position - Camera.main.transform.position);
    }
}
