using UnityEngine;
using UnityEngine.AI;

public class AIUnitManager : MonoBehaviour
{
    [SerializeField] private Unit[] _allUnits = new Unit[9];
    private FormationController _formation;
    private Transform _clickPosition;
    private Vector3 _oldPosition;
    public void Init()
    {
        TakeTroopUnits();
        _formation = GetComponentInChildren<FormationController>();
    }

    private void TakeTroopUnits()
    {
        _allUnits = GetComponentsInChildren<Unit>();

    }
    private void UnitsMoveToPositions()
    {
        foreach (var unit in _allUnits)
        {
            foreach (var nearPlace in _formation.AllUnitsPlaces)
            {
                if (unit.transform == nearPlace.UnitInPlace)
                {
                    unit.GetComponent<NavMeshAgent>().SetDestination(nearPlace.transform.position);
                }
            }
        }
    }
    public void SetFormationPosition(Transform clickPosition)
    {
        _oldPosition = _formation.transform.position;
        _formation.gameObject.transform.position = clickPosition.position;
    }
    public Vector3 GetOldPosition()
    {
        return _oldPosition;
    }
    public void SetDestination()
    {
        UnitsMoveToPositions();
    }

}