using UnityEngine;

public class ObjectiveArrow : MonoBehaviour
{
    [SerializeField] private Transform _arrowPivot;
    private Transform _objective;

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
        float distance = Vector3.Distance(transform.position, _objective.position);
        if (distance < 1f)
        {
            _arrowPivot.gameObject.SetActive(false);
            return;
        }
        else if (!_arrowPivot.gameObject.activeInHierarchy)
        {
            _arrowPivot.gameObject.SetActive(true);
        }
        // Calculate the direction from the Player to the Objective
        Vector3 direction = (_objective.position - transform.position).normalized;
        // Ignore Y-axis difference, as it's a top-down game
        direction.y = 0;
        // Calculate the rotation required to face the direction
        _arrowPivot.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}