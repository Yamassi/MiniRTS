using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider), typeof(ColliderManager), typeof(UnitStatisticsController))]
[RequireComponent(typeof(EquipmentController))]
public abstract class UnitSystem : MonoBehaviour, IStationStateSwitcher, IDamageable
{
    [SerializeField] protected float _currentHealth;
    protected ColliderManager _colliderController;
    protected UnitEventManager _unitEventManager;
    protected BuildingsEventManager _buildingEventManager;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected Rigidbody _rigidbody;
    protected UnitStatisticsController _statisticsController;
    protected EquipmentController _equipmentController;
    protected UnitState _currentUnitState;
    protected List<UnitState> _allStates;
    private bool _isDead = false;
    private bool _isAttack = false;
    protected Coroutine _barrackBuild;
    protected Coroutine _extractWood;
    protected Coroutine _conquerTower;
    protected Coroutine _lookCoroutine;
    protected float _speed;
    protected float _coolDownTimer;
    protected float _timerAfterAttack;
    protected bool _isFirstTimeAttack = true;
    protected bool _isFirstTimeStop = true;
    public bool IsAttack { get => _isAttack; set => _isAttack = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
    public float Speed { get => _speed; set => _speed = value; }
    protected const float motionSmoothTime = .1f;

    public void SwitchToWarrior()
    {
        SwitchState<UnitWarriorState>();
    }
    public void SwitchToWorker()
    {
        SwitchState<UnitWorkerState>();
    }
    public void SwitchToStealth()
    {
        SwitchState<UnitStealthState>();
    }
    protected void SwitchToDead()
    {
        SwitchState<UnitDeadState>();
    }
    public void UpdateWorker()
    {
        _coolDownTimer += Time.deltaTime;
    }
    public void UpdateWarrior()
    {
        _coolDownTimer += Time.deltaTime;
        if (_isFirstTimeAttack)
        {
            _timerAfterAttack += Time.deltaTime;
        }

        if (_speed > 0.4f)
        {
            _colliderController.ChaseCollider.IsMoving = true;
        }
        else if (_speed <= 0.4f)
        {
            _colliderController.ChaseCollider.IsMoving = false;
        }
    }
    public void CheckUnitHealth()
    {
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            SwitchState<UnitDeadState>();
        }
    }
    public void TakeDamage(float damage) => _currentHealth -= damage;
    protected void AddHealth(float damage) => _currentHealth += damage;
    public void Attack()
    {
        if (_isDead == false)
        {
            _navMeshAgent.isStopped = true;
        }
        _isAttack = true;
        _animator.SetBool(Animations.Attack, true);
    }
    public void MeleeAttack(AIUnit unit, float coolDownTimer)
    {
        RotateToTarget(unit.transform);

        if (coolDownTimer > _statisticsController.AttackCoolDown)
        {
            Attack();
            coolDownTimer = 0;
        }
    }
    public void MoveTo(Vector3 targetPosition)
    {
        _navMeshAgent.SetDestination(targetPosition);
        _navMeshAgent.isStopped = false;
    }
    public void StartAnimatorDisableAfter(float seconds)
    {
        StartCoroutine(AnimatorDisableAfter(seconds));
    }
    protected IEnumerator AnimatorDisableAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _animator.enabled = false;
        this.enabled = false;
    }
    public void RotateToTarget(Transform target)
    {
        if (_lookCoroutine != null)
        {
            StopCoroutine(_lookCoroutine);
        }

        _lookCoroutine = StartCoroutine(RotateTo(target));
    }
    private IEnumerator RotateTo(Transform target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position);
        float time = 0;
        float rotationSpeed = 3;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            time += Time.deltaTime;

            yield return null;
        }
    }
    public void Conquer(TowerController tower)
    {
        // if (!IsDead && _swordsmanState == SwordsManState.Warrior)
        // {
        _navMeshAgent.isStopped = false;
        RotateToTarget(tower.transform);

        _navMeshAgent.destination = tower.transform.position;

        _conquerTower = StartCoroutine(ConquerThis(tower));
    }
    IEnumerator ConquerThis(TowerController tower)
    {
        var distance = (transform.position - tower.transform.position).magnitude;
        var towerDestination = _navMeshAgent.destination;
        while (distance > _statisticsController.ExtractRange)
        {
            if (_navMeshAgent.destination != towerDestination)
            {
                StopCoroutine(_conquerTower);
            }
            RotateToTarget(tower.transform);
            distance = (transform.position - tower.transform.position).magnitude;
            yield return null;
        }
        if (distance < _statisticsController.ExtractRange)
        {
            _navMeshAgent.isStopped = true;
            if (!tower.IsConquered)
            {
                tower.StartConquer();
                while (!tower.IsConquered)
                {
                    if (_navMeshAgent.isStopped == false)
                    {
                        tower.StopConquer();
                        StopCoroutine(_conquerTower);
                    }
                    yield return null;
                }
            }
        }

        yield return null;

    }
    public void Die()
    {
        _isDead = true;
        _animator.SetTrigger(Animations.Die);
        _navMeshAgent.enabled = false;
        _colliderController.UnitCollider.enabled = false;
        transform.parent = null;
        int Default = LayerMask.NameToLayer("Default");
        gameObject.layer = Default;

        StartAnimatorDisableAfter(2f);
    }
    public void Chase(AIUnit unit)
    {
        if (unit.IsDead == false)
        {
            if (_speed >= 0.1f)
            {
                RotateToTarget(unit.transform);
            }


            if (_coolDownTimer > _statisticsController.AttackCoolDown)
            {
                _navMeshAgent.destination = unit.transform.position;
            }

            var distance = (transform.position - unit.transform.position).magnitude;

            if (distance <= _statisticsController.AttackRange && _colliderController.AttackCollider.NumColliders > 0)
            {
                MeleeAttack(unit, _coolDownTimer);
                _isFirstTimeStop = true;
                _isFirstTimeAttack = true;

            }
            else if (_colliderController.AttackCollider.NumColliders == 0 && _navMeshAgent.isStopped && _timerAfterAttack >= 2f)
            {
                _isFirstTimeAttack = false;
                _timerAfterAttack = 0;
                if (_isFirstTimeStop)
                {
                    _navMeshAgent.isStopped = false;
                    _isFirstTimeStop = false;
                }
            }
        }
    }
    public void ChopWood(WoodController wood)
    {
        _navMeshAgent.isStopped = false;
        RotateToTarget(wood.transform);

        _navMeshAgent.destination = wood.transform.position;
        _equipmentController.SwitchToChopWorker();
        _extractWood = StartCoroutine(ChopThisWood(wood));

    }
    private IEnumerator ChopThisWood(WoodController wood)
    {

        var distance = (transform.position - wood.transform.position).magnitude;
        var woodDestination = _navMeshAgent.destination;

        while (distance > _statisticsController.ExtractRange && wood.WoodCount > 0)
        {
            if (_navMeshAgent.destination != woodDestination)
            {
                StopCoroutine(_extractWood);
            }
            RotateToTarget(wood.transform);
            distance = (transform.gameObject.transform.position - wood.transform.position).magnitude;
            yield return null;
        }
        if (distance < _statisticsController.ExtractRange)
        {
            _navMeshAgent.isStopped = true;

            while (wood != null)
            {
                if (_coolDownTimer > _statisticsController.ExtractCoolDown && wood.WoodCount > 0)
                {
                    RotateToTarget(wood.transform);
                    StartCoroutine(AddWood(wood));

                    _animator.SetTrigger(Animations.WoodHit);

                    _coolDownTimer = 0;
                }
                if (_navMeshAgent.isStopped == false)
                {
                    StopCoroutine(_extractWood);
                }
                yield return null;
            }
        }
        yield return null;

    }
    private IEnumerator AddWood(WoodController wood)
    {
        yield return new WaitForSeconds(2.3f);
        _unitEventManager.ChopWood(wood, _statisticsController.WoodChopAtOnce);
    }

    public void BuildBarrack(BarracksController barrack)
    {
        _navMeshAgent.isStopped = false;
        RotateToTarget(barrack.transform);
        _equipmentController.SwitchToConstructWorker();
        _barrackBuild = StartCoroutine(BuildThisBarrack(barrack));
    }
    private IEnumerator BuildThisBarrack(BarracksController barrack)
    {
        var distance = (transform.position - barrack.transform.position).magnitude;
        var barrackDestination = _navMeshAgent.destination;

        while (distance > _statisticsController.ExtractRange + 1)
        {
            if (_navMeshAgent.destination != barrackDestination)
            {
                _unitEventManager.CancelBuildBarracks(barrack);
                // barrack.StopBuild();
                _equipmentController.SwitchToChopWorker();
                StopCoroutine(_barrackBuild);

            }
            RotateToTarget(barrack.transform);
            distance = (transform.position - barrack.transform.position).magnitude;
            yield return null;
        }
        if (distance < _statisticsController.ExtractRange + 1)
        {
            _navMeshAgent.isStopped = true;

            _unitEventManager.StartBuildBarracks(barrack);
            // barrack.StartBuildBarrack();

            while (!barrack.IsCompleted)
            {
                RotateToTarget(barrack.transform);
                if (_coolDownTimer > _statisticsController.ExtractCoolDown && !barrack.IsCompleted)
                {
                    _animator.SetTrigger(Animations.WoodHit);
                    _coolDownTimer = 0;
                }
                if (_navMeshAgent.isStopped == false)
                {
                    _equipmentController.SwitchToChopWorker();
                    _unitEventManager.CancelBuildBarracks(barrack);
                    // barrack.StopBuild();
                    StopCoroutine(_barrackBuild);
                }
                yield return null;
            }

        }

        yield return null;

    }

    public void SwitchState<T>() where T : UnitState
    {
        var state = _allStates.FirstOrDefault(s => s is T);
        _currentUnitState?.ExitState();
        _currentUnitState = state;
        _currentUnitState.EnterState();
    }

}



public interface IStationStateSwitcher
{
    void SwitchState<T>() where T : UnitState;
}