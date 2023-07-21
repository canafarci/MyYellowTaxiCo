using System;
using UnityEngine;

namespace Taxi.WaitZones
{
    [CreateAssetMenu(fileName = "WaitZoneConfigSO", menuName = "NewYork Taxi/WaitZoneConfigSO", order = 0)]
    public class WaitZoneConfigSO : ScriptableObject
    {
        public Action OnSuccess { get { return _onSuccess; } }
        private Action _onSuccess;
        public WaitZoneConfigSO(Action OnSuccess)
        {
            _onSuccess = OnSuccess;
        }
    }
}