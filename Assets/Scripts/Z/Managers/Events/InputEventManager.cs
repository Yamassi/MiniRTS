using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InputEventManager : ScriptableObject
{
    public delegate void Chop(WoodController wood, Vector3 clickPosition);
    public event Chop OnChopWood;
    public void ChopWood(WoodController wood, Vector3 clickPosition) => OnChopWood?.Invoke(wood, clickPosition);

    public delegate void Conquer(TowerController tower, Vector3 clickPosition);
    public event Conquer OnConquerTower;
    public void ConquerTower(TowerController tower, Vector3 clickPosition) => OnConquerTower?.Invoke(tower, clickPosition);

    public delegate void BuildBarracks(BarracksController barracks, Vector3 clickPosition);
    public event BuildBarracks OnBuildBarracks;
    public void BuildBarrack(BarracksController barracks, Vector3 clickPosition) => OnBuildBarracks?.Invoke(barracks, clickPosition);

    public delegate void UnitAttack(AIUnit unit, Vector3 clickPosition);
    public event UnitAttack OnAttackUnit;
    public void AttackUnit(AIUnit unit, Vector3 clickPosition) => OnAttackUnit?.Invoke(unit, clickPosition);

    public delegate void UnitMove(Vector3 clickPosition);
    public event UnitMove OnUnitMove;
    public void MoveUnit(Vector3 clickPosition) => OnUnitMove?.Invoke(clickPosition);
}
