using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class UnitWorkerState : UnitState
{
    public UnitWorkerState(ColliderManager colliderController,
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
        _equipmentController.SwitchToChopWorker();
        _colliderController.ChaseCollider.OnAIUnitChase -= _unit.Chase;
    }
    public override void UpdateState()
    {
        _unit.CheckUnitHealth();
        _unit.UpdateWorker();
    }
    public override void ExitState()
    {

    }
    public override void OnTrigger()
    {

    }


}
