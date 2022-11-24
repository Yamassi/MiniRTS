using UnityEngine;

public class AISwordsMan : AIUnit
{
    protected UnitAttackColliderController _unitAttackCollider;
    protected UnitChaseColliderController _unitChaseCollider;
    private float _attackCoolDownTimer;
    private SwordController _sword;
    private bool _firstTimeStop = true;
    private bool _firstTimeAttack = true;
    private float _timerAfterAttack;
    private void Start()
    {
        _unitChaseCollider = GetComponentInChildren<UnitChaseColliderController>();
        _unitAttackCollider = GetComponentInChildren<UnitAttackColliderController>();
        _sword = GetComponentInChildren<SwordController>();
        _sword.Init(AttackPower);

        _unitChaseCollider.OnUnitChase += Chase;
        _attackCoolDownTimer = _attackCoolDown;
    }
    private void OnDisable()
    {
        _sword.gameObject.SetActive(false);
    }
    public override void Update()
    {
        CheckUnitHealth();
        _animator.SetFloat(Animations.Speed, _navMeshAgent.velocity.magnitude);
        _attackCoolDownTimer += Time.deltaTime;
        _speed = _navMeshAgent.velocity.magnitude / _navMeshAgent.speed;

        if (_firstTimeAttack)
        {
            _timerAfterAttack += Time.deltaTime;
        }
    }

    public void Chase(UnitSystem unit)
    {
        if (unit.IsDead == false && _isDead == false)
        {
            if (_speed >= 0.1f)
            {
                RotateToTarget(unit.transform);
            }
            if (_attackCoolDownTimer > _attackCoolDown)
            {
                _navMeshAgent.destination = unit.transform.position;
            }

            var distance = (_unit.position - unit.transform.position).magnitude;

            if (distance <= _attackRange && _unitAttackCollider.NumColliders > 0)
            {
                MeleeAttack(unit);
                _firstTimeStop = true;
                _firstTimeAttack = true;

            }
            else if (_unitAttackCollider.NumColliders == 0 && _navMeshAgent.isStopped && _timerAfterAttack >= 2f)
            {
                _firstTimeAttack = false;
                _timerAfterAttack = 0;
                if (_firstTimeStop)
                {
                    _navMeshAgent.isStopped = false;
                    _firstTimeStop = false;
                }

            }
        }
    }
    private void MeleeAttack(UnitSystem unit)
    {
        RotateToTarget(unit.transform);
        if (_attackCoolDownTimer > _attackCoolDown)
        {
            Attack();
            _attackCoolDownTimer = 0;
        }
    }
    private void WeaponOn()
    {
        _sword.SwordOn();
    }
    private void WeaponOff()
    {
        _sword.SwordOff();
    }
}
