using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIUnit : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;
    [SerializeField] protected float _attackRange;
    [SerializeField] private float _attackPower;
    [SerializeField] protected float _attackCoolDown;
    [SerializeField] protected float _rotationSpeedToTarget;
    protected UnitsManager _troop;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected CapsuleCollider _capsuleCollider;
    protected Transform _unit;
    [SerializeField] protected bool _isDead = false;
    protected bool _isAttack = false;
    bool isAgreeToAttack;
    public float AttackPower { get => _attackPower; }
    public UnitsManager Troop { get => _troop; set => _troop = value; }
    public bool IsAgreeToAttack { get => isAgreeToAttack; set => isAgreeToAttack = value; }
    public bool IsDead { get => _isDead; }
    Coroutine _lookCoroutine;
    public delegate void Died(AIUnit unit);
    public event Died OnDied;
    protected float _speed;
    private void Awake()
    {
        _unit = transform;
        _currentHealth = _maxHealth;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
    public virtual void Update()
    {
        CheckUnitHealth();
        _animator.SetFloat(Animations.Speed, _navMeshAgent.velocity.magnitude);
    }
    public void AnimateTo(int animation)
    {
        _animator.SetTrigger(animation);
    }
    public virtual void CheckUnitHealth()
    {
        if (_currentHealth <= 0)
        {
            _isDead = true;
            Die();
        }
    }
    public void Die()
    {
        _isDead = true;
        _animator.SetTrigger(Animations.Die);
        _navMeshAgent.enabled = false;
        _capsuleCollider.enabled = false;
        int Default = LayerMask.NameToLayer("Default");
        gameObject.layer = Default;
        OnDied?.Invoke(this);

        StartCoroutine(AnimatorDisableAfter(2f));
    }

    #region Move
    public virtual void RotateToTarget(Transform target)
    {
        // Vector3 newDirection = Vector3.RotateTowards(_unit.forward, target - _unit.position, _rotationSpeedToTarget * Time.deltaTime, 0.0f);
        // _unit.rotation = Quaternion.LookRotation(newDirection);


        if (_lookCoroutine != null)
        {
            StopCoroutine(_lookCoroutine);
        }

        _lookCoroutine = StartCoroutine(RotateTo(target));
    }
    public void MoveTo(Vector3 targetPosition)
    {
        _navMeshAgent.SetDestination(targetPosition);
        _navMeshAgent.isStopped = false;
    }
    #endregion
    public virtual void Attack()
    {
        if (_isDead == false)
        {
            _navMeshAgent.isStopped = true;
        }
        _isAttack = true;
        _animator.SetBool(Animations.Attack, true);
    }
    #region Health
    public void AddDamage(float damage) => _currentHealth -= damage;
    public void TakeDamage(float damage)
    {
        AddDamage(damage);
    }
    public void AddHealth(float damage) => _currentHealth += damage;
    #endregion
    IEnumerator AnimatorDisableAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _animator.enabled = false;
        this.enabled = false;
    }
    IEnumerator RotateTo(Transform target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(target.position.x, _unit.position.y, target.position.z) - _unit.position);
        float time = 0;
        float rotationSpeed = 3;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            time += Time.deltaTime;

            yield return null;
        }
    }
    IEnumerator NextMoveAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // _troop.UnitsMoveToPositions();
    }
}