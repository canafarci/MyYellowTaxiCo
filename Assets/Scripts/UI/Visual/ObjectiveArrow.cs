using System;
using TaxiGame.GameState;
using UnityEngine;
using Zenject;

namespace TaxiGame.UI.Visuals
{
    public class ObjectiveArrow : MonoBehaviour
    {
        [SerializeField] private Transform _arrowPivot;
        private Transform _objective;
        private ProgressionState _progressionState;

        [Inject]
        private void Init(ProgressionState progressionState)
        {
            _progressionState = progressionState;
        }
        private void Start()
        {
            _progressionState.OnFirstVIPSpawned += ProgressionState_FirstVIPSpawnedHandler;
        }

        private void ProgressionState_FirstVIPSpawnedHandler(object sender, OnFirstVIPSpawnedArgs e)
        {
            ChangeObjective(e.Target);
        }

        public void DisableArrow()
        {
            _arrowPivot.gameObject.SetActive(false);
            _objective = null;
        }

        public void ChangeObjective(Transform newObjective)
        {
            _arrowPivot.gameObject.SetActive(true);
            _objective = newObjective;
        }
        private void LateUpdate()
        {
            if (_objective == null) return;

            UpdateArrow();
        }

        private void UpdateArrow()
        {
            float distanceSquared = (_objective.position - transform.position).sqrMagnitude;

            if (distanceSquared < 1f) // Use squared distance for comparison
            {
                _arrowPivot.gameObject.SetActive(false);
            }
            else
            {
                _arrowPivot.gameObject.SetActive(true);
                // Calculate the direction from the Player to the Objective
                Vector3 direction = _objective.position - transform.position;
                direction.y = 0; // Ignore Y-axis difference, as it's a top-down game
                direction.Normalize(); // Normalize the direction vector

                // Calculate the rotation required to face the direction
                _arrowPivot.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }
        }
    }
}