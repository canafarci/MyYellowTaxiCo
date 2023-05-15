using UnityEngine;

public class ObjectiveArrow : MonoBehaviour
{
    public Transform Objective;
    [SerializeField] private Transform _arrowPivot;
    private float _updateTimer;
    Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void DisableArrow()
    {
        _arrowPivot.gameObject.SetActive(false);
        Objective = null;
    }

    public void ChangeObjective(Transform newObjective)
    {
        _arrowPivot.gameObject.SetActive(true);
        Objective = newObjective;
    }

    private void Update()
    {

    }

    private void LateUpdate()
    {
        if (Objective == null) return;

        UpdateArrow();
    }

    private void UpdateArrow()
    {
        float distance = Vector3.Distance(transform.position, Objective.position);
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
        Vector3 direction = (Objective.position - transform.position).normalized;
        // Ignore Y-axis difference, as it's a top-down game
        direction.y = 0;
        // Calculate the rotation required to face the direction
        _arrowPivot.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}