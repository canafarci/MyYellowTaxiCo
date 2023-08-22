using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaxiGame.Vehicles.Repair
{
    public interface IHandleHolder
    {
        public void Clear();
        public void SetHandle(Handle handle);
        public Transform GetTransform();
    }
}
