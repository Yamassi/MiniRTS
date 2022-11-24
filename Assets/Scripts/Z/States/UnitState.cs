using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public abstract class UnitState
{
    protected ColliderManager _colliderController;
    protected UnitEventManager _eventManager;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected Rigidbody _rigidbody;
    protected UnitStatisticsController _statisticsController;
    protected EquipmentController _equipmentController;
    protected UnitSystem _unit;
    protected IStationStateSwitcher _stateSwitcher;

    protected UnitState(
        ColliderManager colliderController,
        UnitEventManager eventManager,
        NavMeshAgent navMeshAgent,
        Animator animator,
        Rigidbody rigidbody,
        UnitStatisticsController statisticsController,
        EquipmentController equipmentController,
        UnitSystem unitSystem,
        IStationStateSwitcher stateSwitcher)
    {
        _colliderController = colliderController;
        _eventManager = eventManager;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _rigidbody = rigidbody;
        _statisticsController = statisticsController;
        _equipmentController = equipmentController;
        _stateSwitcher = stateSwitcher;
        _unit = unitSystem;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void OnTrigger();

}
