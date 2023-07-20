using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUnlocker : MonoBehaviour
{
    InputReader _reader;
    Queue<IEnumerator> _camQueue = new Queue<IEnumerator>();
    Coroutine _dequeueCoroutine;

    private void Awake() => _reader = FindObjectOfType<InputReader>();
    public void StartCameraRoutine(GameObject camera, float cameraDuration)
    {
        _camQueue.Enqueue(CameraRoutine(camera, cameraDuration));

        if (_dequeueCoroutine == null)
            _dequeueCoroutine = StartCoroutine(Dequeue());
    }
    IEnumerator CameraRoutine(GameObject camera, float cameraDuration)
    {
        _reader.Disable();
        camera.SetActive(true);
        yield return new WaitForSeconds(cameraDuration);
        camera.SetActive(false);
        _reader.Enable();
    }

    IEnumerator Dequeue()
    {
        while (_camQueue.Count > 0)
        {
            yield return StartCoroutine(_camQueue.Dequeue());
        }

        _dequeueCoroutine = null;
    }
}
