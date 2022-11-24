using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : UnitSystem
{
    public void Init(UnitEventManager eventManager, BuildingsEventManager buildingsEventManager)
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _colliderController = GetComponent<ColliderManager>();
        _statisticsController = GetComponent<UnitStatisticsController>();
        _equipmentController = GetComponent<EquipmentController>();
        _unitEventManager = eventManager;
        _buildingEventManager = buildingsEventManager;

        _colliderController.Init();
        _equipmentController.Init(_statisticsController);

        _currentHealth = _statisticsController.MaxHealth;

        _allStates = new List<UnitState>()
        {
                new UnitWarriorState(_colliderController, _unitEventManager, _navMeshAgent, _animator, _rigidbody, _statisticsController, _equipmentController,this, this),
                new UnitWorkerState(_colliderController, _unitEventManager, _navMeshAgent, _animator, _rigidbody, _statisticsController, _equipmentController,this, this),
                new UnitStealthState(_colliderController, _unitEventManager, _navMeshAgent, _animator, _rigidbody, _statisticsController, _equipmentController,this, this),
                new UnitDeadState(_colliderController, _unitEventManager, _navMeshAgent, _animator, _rigidbody, _statisticsController, _equipmentController,this, this),
        };

        _currentUnitState = _allStates[0];
        _currentUnitState.EnterState();
    }
    private void Update()
    {
        _speed = _navMeshAgent.velocity.magnitude / _navMeshAgent.speed;
        _animator.SetFloat(Animations.Speed, _speed);
        _currentUnitState.UpdateState();
    }

    void WeaponOn()
    {
        _equipmentController.WeaponOn();
    }
    void WeaponOff()
    {
        _equipmentController.WeaponOff();
    }
}
