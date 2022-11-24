using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _extractRange;
    [SerializeField] private float _attackPower;
    [SerializeField] protected float _attackCoolDown;
    [SerializeField] protected float _extractCoolDown;
    [SerializeField] GameObject _selectGFX;
    [SerializeField] protected UnitsManager _troop;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    protected CapsuleCollider _capsuleCollider;
    [SerializeField] protected CapsuleCollider _clickCollider;
    protected Transform _unit;
    [SerializeField] protected bool _isDead = false;
    protected bool _isAttack = false;
    public float AttackPower { get => _attackPower; }
    public UnitsManager Troop { get => _troop; set => _troop = value; }
    public bool IsDead { get => _isDead; }

    Coroutine _lookCoroutine;
    public delegate void Died(Unit unit);
    public event Died OnDied;
    float motionSmoothTime = .1f;
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
        float speed = _navMeshAgent.velocity.magnitude / _navMeshAgent.speed;
        _animator.SetFloat(Animations.Speed, speed, motionSmoothTime, Time.deltaTime);
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
        _clickCollider.enabled = false;
        transform.parent = null;
        int Default = LayerMask.NameToLayer("Default");
        gameObject.layer = Default;
        OnDied?.Invoke(this);

        StartCoroutine(AnimatorDisableAfter(2f));
    }

    #region Move
    public virtual void RotateToTarget(Transform target)
    {
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
    public void TakeDamage(float damage) => _currentHealth -= damage;
    public void AddHealth(float damage) => _currentHealth += damage;
    #endregion
    public void SelectUnit()
    {
        _selectGFX.SetActive(true);
    }
    public void DeSelectUnit()
    {
        _selectGFX.SetActive(false);
    }
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
}