using System.Collections.Generic;
using UnityEngine;

public class FormationsManager : MonoBehaviour
{
    [SerializeField] private List<UnitController> _unitsInFormation;
    [SerializeField] private FormationController[] _formations;
    [SerializeField] private FormationController _activeFormation;
    public FormationController ActiveFormation
    {
        get => _activeFormation;
        set => _activeFormation = value;
    }
    private FlagHandler _flagHandler;
    public void Init(List<UnitController> allUnits)
    {
        foreach (var formation in _formations)
        {
            formation.Init(allUnits);
        }
    }

    // public void UpdateFlagHandler()
    // {
    //     _flagHandler.SetNewFlagHandler();
    // }

    public void UpdateUnitsInFormation(List<UnitController> units)
    {
        foreach (var unitPlace in _activeFormation.AllUnitsPlaces)
        {
            unitPlace.UnitInPlace = null;
        }
        _unitsInFormation = units;
    }
    public void SetActiveFormation(int formationNumber)
    {
        foreach (FormationController formation in _formations)
        {
            if (formation.FormationNumber == formationNumber)
            {
                _activeFormation.gameObject.SetActive(false);
                _activeFormation = formation;
                _activeFormation.gameObject.SetActive(true);

            }
        }
    }
    public void UpdateActiveFormation()
    {
        _activeFormation.SetNewUnitPlaces(_unitsInFormation);
        _flagHandler = _activeFormation.FlagHandler;
    }
}
