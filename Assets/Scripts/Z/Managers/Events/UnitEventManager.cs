using UnityEngine;
using System;

[CreateAssetMenu]
public class UnitEventManager : ScriptableObject
{
    public Action OnSwitchToWarrior;
    public Action OnSwitchToWorker;
    public Action OnSwitchToStealth;
    public delegate void Attack(UnitSystem unit);
    public event Attack OnAttack;
    public delegate void Died(UnitController unit);
    public event Died OnDied;
    public delegate void WoodChop(WoodController wood, int woodChopAtOnce);
    public event WoodChop OnWoodChop;
    public delegate void UnitAdd(Vector3 position);
    public event UnitAdd OnAddUnit;
    public delegate void BarrackBuild(BarracksController barracks);
    public event BarrackBuild OnBarracksBuild;
    public delegate void CancelBarrackBuild(BarracksController barracks);
    public event CancelBarrackBuild OnCancelBarracksBuild;

    public void UnitSwitchToWarrior() => OnSwitchToWarrior?.Invoke();
    public void UnitSwitchToWorker() => OnSwitchToWorker?.Invoke();
    public void UnitSwitchToStealth() => OnSwitchToStealth?.Invoke();
    public void UnitAttack(UnitSystem unit) => OnAttack?.Invoke(unit);
    public void UnitDied(UnitController unit) => OnDied?.Invoke(unit);
    public void ChopWood(WoodController wood, int woodChopAtOnce) => OnWoodChop(wood, woodChopAtOnce);
    public void AddUnit(Vector3 position) => OnAddUnit?.Invoke(position);
    public void StartBuildBarracks(BarracksController barracks) => OnBarracksBuild?.Invoke(barracks);
    public void CancelBuildBarracks(BarracksController barracks) => OnCancelBarracksBuild?.Invoke(barracks);

}
