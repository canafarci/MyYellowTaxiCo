using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraUnlocker : MonoBehaviour
{
    private IInputReader _reader;
    private Queue<IEnumerator> _camQueue = new Queue<IEnumerator>();
    private Coroutine _dequeueCoroutine;

    [Inject]
    private void Init(IInputReader reader)
    {
        _reader = reader;
    }
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
