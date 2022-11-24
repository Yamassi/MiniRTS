using System.Collections.Generic;
using UnityEngine;

public class UnitsManager : MonoBehaviour
{
    [SerializeField] private List<UnitController> _allUnits;
    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private FlagController _flagController;
    public List<UnitController> AllUnits { get => _allUnits; set => _allUnits = value; }
    public GameObject UnitPrefab { get => _unitPrefab; }
    private UnitEventManager _unitEventManager;
    private BuildingsEventManager _buildingsEventManager;
    public enum UnitsState
    {
        Warrior,
        Worker,
        Stealth
    }
    public UnitsState unitsState = UnitsState.Warrior;
    public void Init(UnitEventManager unitEventManager, BuildingsEventManager buildingsEventManager)
    {
        _unitEventManager = unitEventManager;
        _buildingsEventManager = buildingsEventManager;
        foreach (var unit in _allUnits)
        {
            unit.Init(unitEventManager, buildingsEventManager);
        }

    }
    public void AddUnit(UnitController unit)
    {
        _allUnits.Add(unit);
    }
    public void RemoveUnit(UnitController unit)
    {
        _allUnits.Remove(unit);
    }
    public void UpdateFlagHandler()
    {
        _flagController.gameObject.transform.SetParent(_allUnits[0].transform);
    }
}


