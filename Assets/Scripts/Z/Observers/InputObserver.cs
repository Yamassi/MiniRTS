using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputObserver : Observer
{
    public void Init(
            InputEventManager inputEventManager,
            UnitsManager unitsManager,
            CursorController cursorController,
            UnitsObserver unitsObserver)
    {
        _inputEventManager = inputEventManager;
        _unitsManager = unitsManager;
        _cursorController = cursorController;
        _unitObserver = unitsObserver;

        inputEventManager.OnChopWood += ChopWood;
        inputEventManager.OnConquerTower += ConquerTower;
        inputEventManager.OnBuildBarracks += BuildBarrack;
        inputEventManager.OnAttackUnit += ToAttack;
        inputEventManager.OnUnitMove += MoveUnitsTo;
    }
    private void ChopWood(WoodController wood, Vector3 clickPosition)
    {
        if (_unitsManager.unitsState != UnitsManager.UnitsState.Stealth && _unitsManager.unitsState != UnitsManager.UnitsState.Warrior)
        {
            _cursorController.CursorMoveToExtract(clickPosition);
            _unitObserver.ExtractWood(wood);
        }
    }
    private void ConquerTower(TowerController tower, Vector3 clickPosition)
    {
        if (!tower.IsConquered && _unitsManager.unitsState != UnitsManager.UnitsState.Stealth && _unitsManager.unitsState != UnitsManager.UnitsState.Worker)
        {
            _cursorController.CursorMoveToConquer(clickPosition);
            _unitObserver.Conquer(tower);
        }
    }
    private void BuildBarrack(BarracksController barracks, Vector3 clickPosition)
    {
        if (!barracks.IsCompleted && _unitsManager.unitsState != UnitsManager.UnitsState.Stealth && _unitsManager.unitsState != UnitsManager.UnitsState.Warrior)
        {
            _cursorController.CursorMoveToBuild(clickPosition, barracks.transform.position.y);
            _unitObserver.BuildBarrack(barracks);
        }
    }

    private void ToAttack(AIUnit unit, Vector3 clickPosition)
    {
        _cursorController.CursorMoveToAttack(clickPosition);
        _unitObserver.AttackUnit(unit);
    }

    private void MoveUnitsTo(Vector3 clickPosition)
    {
        _cursorController.CursorMoveToPoint(clickPosition);
        _unitObserver.MoveUnitsInGridToNewPlace(clickPosition);
    }

}
