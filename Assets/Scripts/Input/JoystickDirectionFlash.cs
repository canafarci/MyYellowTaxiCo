using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickDirectionFlash : MonoBehaviour
{
    InputReader _reader;
    GameObject[] _allImages = new GameObject[4];
    [SerializeField] GameObject _leftUp, _leftDown, _rightUp, _rightDown;
    private void Awake()
    {
        _reader = GetComponent<InputReader>();
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
