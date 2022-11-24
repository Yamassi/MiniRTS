using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitDeadState : UnitState
{
    public UnitDeadState(ColliderManager colliderController,
        UnitEventManager eventManager,
        NavMeshAgent navMeshAgent,
        Animator animator,
        Rigidbody rigidbody,
        UnitStatisticsController statisticsController,
        EquipmentController equipmentController,
         UnitSystem unitSystem,
        IStationStateSwitcher stateSwitcher) : base(
            colliderController,
            eventManager,
            navMeshAgent,
            animator,
            rigidbody,
            statisticsController,
            equipmentController,
            unitSystem,
            stateSwitcher)
    { }
    public override void EnterState()
    {
        _colliderController.ChaseCollider.OnAIUnitChase -= _unit.Chase;
        _unit.Die();
        _eventManager.UnitDied(_unit.gameObject.GetComponent<UnitController>());
    }
    public override void UpdateState()
    {

    }
    public override void ExitState()
    {

    }
    public override void OnTrigger()
    {

    }

}
