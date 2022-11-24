using UnityEngine;
using UnityEngine.AI;

public class UnitWarriorState : UnitState
{

    public UnitWarriorState(ColliderManager colliderController,
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
        _equipmentController.SwitchToWarrior();
        _colliderController.ChaseCollider.OnAIUnitChase += _unit.Chase;
    }
    public override void UpdateState()
    {
        _unit.CheckUnitHealth();
        _unit.UpdateWarrior();
    }
    public override void ExitState()
    {

    }
    public override void OnTrigger()
    {

    }


}
