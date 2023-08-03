using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JoystickDirectionFlash : MonoBehaviour
{
    private IInputReader _reader;
    [SerializeField] private GameObject _leftUp, _leftDown, _rightUp, _rightDown;
    private GameObject[] _allImages = new GameObject[4];

    [Inject]
    private void Init(IInputReader reader)
    {
        _reader = reader;
        print("calledflash");
        print(_reader);
        _allImages[0] = _rightUp;
        _allImages[1] = _rightDown;
        _allImages[2] = _leftUp;
        _allImages[3] = _leftDown;
    }

    private void FixedUpdate()
    {
        Vector2 input = _reader.ReadInput();

        if (input == Vector2.zero)
            DisableExcept(999);
        else if (input.x > 0 && input.y > 0)
            DisableExcept(0);
        else if (input.x > 0 && input.y < 0)
            DisableExcept(1);
        else if (input.x < 0 && input.y > 0)
            DisableExcept(2);
        else if (input.x < 0 && input.y < 0)
            DisableExcept(3);
    }

    void DisableExcept(int index)
    {
        for (int i = 0; i < _allImages.Length; i++)
        {
            if (i == index)
                _allImages[i].SetActive(true);
            else
                _allImages[i].SetActive(false);
        }
    }

}
