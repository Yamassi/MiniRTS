using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsObserver : Observer
{
    private Transform _clickPosition;
    private Vector3 _lastPosition;
    private Vector3 _flagOffset = new Vector3(0, 3, 0);

    public void Init(
            InputEventManager inputEventManager,
            UnitEventManager unitEventManager,
            UnitsManager unitsManager,
            UIEventManager uIEventManager,
            BuildingsEventManager buildingsEventManager)
    {
        _inputEventManager = inputEventManager;
        _unitEventManager = unitEventManager;
        _unitsManager = unitsManager;
        _uIEventManager = uIEventManager;
        _buildingEventManager = buildingsEventManager;

        _uIEventManager.OnSwitchToWarrior += SwitchToWarrior;
        _uIEventManager.OnSwitchToWorker += SwitchToWorker;
        _uIEventManager.OnSwitchToStealth += SwitchToStealth;

        _unitEventManager.OnAddUnit += AddUnit;
        _unitEventManager.OnDied += RemoveUnit;

        _unitsManager.UpdateFlagHandler();
    }

    public void ExtractWood(WoodController wood)
    {
        foreach (UnitController unit in _unitsManager.AllUnits)
        {
            if (wood.WoodCount > 0)
            {
                unit.ChopWood(wood);
            }
        }
    }
    public void Conquer(TowerController tower)
    {
        foreach (UnitController unit in _unitsManager.AllUnits)
        {
            unit.Conquer(tower);
        }
    }
    public void BuildBarrack(BarracksController barrack)
    {
        MoveUnitsInCircleToNewPlace(barrack.transform.position);
        foreach (UnitController unit in _unitsManager.AllUnits)
        {
            unit.BuildBarrack(barrack);
        }
    }
    public void AttackUnit(AIUnit aIunit)
    {
        foreach (UnitController unit in _unitsManager.AllUnits)
        {
            //TODO
            // unit.ToAttack(unit);
        }
    }

    public void MoveUnitsInCircleToNewPlace(Vector3 clickPosition)
    {
        float RadiusAroundTarget = 3f;
        for (int i = 0; i < _unitsManager.AllUnits.Count; i++)
        {
            Vector3 target = new Vector3(
             clickPosition.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / _unitsManager.AllUnits.Count),
             clickPosition.y,
             clickPosition.z + RadiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i / _unitsManager.AllUnits.Count)
             );
            _unitsManager.AllUnits[i].MoveTo(target);
        }
    }
    public void MoveUnitsInGridToNewPlace(Vector3 clickPosition)
    {
        int counter = -1;
        int xoffset = -1;

        float sqrt = Mathf.Sqrt(_unitsManager.AllUnits.Count);

        float startx = clickPosition.x;

        for (int i = 0; i < _unitsManager.AllUnits.Count; i++)
        {
            counter++;
            xoffset++;

            if (xoffset > 1)
            {
                xoffset = 1;
            }

            clickPosition = new Vector3(clickPosition.x + (xoffset * 1.0f), 0f, clickPosition.z);

            if (counter == Mathf.Floor(sqrt))
            {
                counter = 0;

                clickPosition.x = startx;

                clickPosition.z += 1 + 0.25f;
            }

            _unitsManager.AllUnits[i].MoveTo(new Vector3(clickPosition.x - 1, 0f, clickPosition.z - 1));
        }
    }

    public void SwitchToWarrior()
    {
        foreach (UnitController unit in _unitsManager.AllUnits)
        {
            unit.SwitchToWarrior();
            _unitsManager.unitsState = UnitsManager.UnitsState.Warrior;
        }


    }
    public void SwitchToWorker()
    {
        foreach (UnitController unit in _unitsManager.AllUnits)
        {
            unit.SwitchToWorker();
            _unitsManager.unitsState = UnitsManager.UnitsState.Worker;
        }
    }
    public void SwitchToStealth()
    {
        foreach (UnitController unit in _unitsManager.AllUnits)
        {
            unit.SwitchToStealth();
            _unitsManager.unitsState = UnitsManager.UnitsState.Worker;
        }
    }
    public void AddUnit(Vector3 position)
    {
        GameObject newUnit = Instantiate(_unitsManager.UnitPrefab, position, Quaternion.identity);
        var unit = newUnit.GetComponent<UnitController>();
        unit.Init(_unitEventManager, _buildingEventManager);

        _unitsManager.AddUnit(unit);
        _unitsManager.UpdateFlagHandler();
    }
    public void RemoveUnit(UnitController unit)
    {
        _unitsManager.RemoveUnit(unit);
        if (_unitsManager.AllUnits.Count > 0)
        {
            _unitsManager.UpdateFlagHandler();
        }
    }

}
