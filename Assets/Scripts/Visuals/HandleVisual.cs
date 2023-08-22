using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace TaxiGame.Visual
{
    public class HandleVisual : MonoBehaviour
    {
        private Handle _handle;

        [Inject]
        private void Init(Handle handle)
        {
            _handle = handle;
        }

        private void Start()
        {
            _handle.OnHandleOwnerChanged += Handle_HandleOwnerChangedHandler;
        }

        private void Handle_HandleOwnerChangedHandler(object sender, HandleOwnerChangedArgs e)
        {
            transform.SetParent(e.Parent);
            transform.DOLocalMove(Vector3.zero, 1f);
            transform.DOLocalRotate(Vector3.zero, 1f);
        }
    }
}
