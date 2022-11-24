using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{
    [SerializeField] private UnitPlace[] _allUnitPlaces;
    [SerializeField] private FlagHandler _flagHandler;
    public UnitPlace[] AllUnitsPlaces
    {
        get => _allUnitPlaces;
        set => value = AllUnitsPlaces;
    }
    [SerializeField] private int _formationNumber;
    public int FormationNumber => _formationNumber;

    public FlagHandler FlagHandler { get => _flagHandler; set => _flagHandler = value; }

    private List<UnitController> _allUnits;
    private void TakeAllUnitPlaces() => _allUnitPlaces = GetComponentsInChildren<UnitPlace>();
    public void Init(List<UnitController> allUnits)
    {
        _allUnits = allUnits;
        SetNewUnitPlaces(_allUnits);
    }
    public void SetNewUnitPlaces(List<UnitController> _unitsInFormation)
    {
        foreach (UnitController unit in _unitsInFormation)
        {
            foreach (UnitPlace unitPlace in _allUnitPlaces)
            {
                if (unitPlace.UnitInPlace == null)
                {
                    unitPlace.UnitInPlace = unit.transform;
                    break;
                }
            }
        }

    }
    private void OnDisable()
    {
        foreach (UnitPlace unitPlace in _allUnitPlaces)
        {
            unitPlace.UnitInPlace = null;
        }
    }
}