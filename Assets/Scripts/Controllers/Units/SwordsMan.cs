using UnityEngine;
using System.Collections;

public class SwordsMan : Unit
{
    [SerializeField] int WoodExtract;
    protected UnitAttackColliderController _unitAttackCollider;
    protected UnitChaseColliderController _unitChaseCollider;
    UnitStealthColliderController _stealthCollider;
    float _coolDownTimer;
    SwordController _sword;
    ShieldController _shield;
    AxeController _axe;
    HammerController _hammer;
    bool _isFirstTimeStop = true;
    bool _isFirstTimeAttack = true;
    public enum SwordsManState
    {
        Warrior,
        Worker,
        Stealth,
    }
    public SwordsManState _swordsmanState = 0;
    float _timerAfterAttack;
    ResourceManager _resources;
    Coroutine _barrackBuild;
    Coroutine _extractWood;
    Coroutine _conquerTower;

    private void OnEnable()
    {
        _unitChaseCollider = GetComponentInChildren<UnitChaseColliderController>();
        _unitAttackCollider = GetComponentInChildren<UnitAttackColliderController>();
        _stealthCollider = GetComponentInChildren<UnitStealthColliderController>();
        _sword = GetComponentInChildren<SwordController>();
        _shield = GetComponentInChildren<ShieldController>();
        _axe = GetComponentInChildren<AxeController>(true);
        _hammer = GetComponentInChildren<HammerController>(true);
        _resources = FindObjectOfType<ResourceManager>();

        _unitChaseCollider.OnAIUnitChase += Chase;
        _stealthCollider.OnUnitDiscovered += ToStealthDiscovered;
        _stealthCollider.OnUnitHided += ToStealthFromDiscovered;
        _coolDownTimer = _attackCoolDown;
    }
    private void OnDisable()
    {
        _sword.gameObject.SetActive(false);
    }

    public override void Update()
    {
        CheckUnitHealth();
        _speed = _navMeshAgent.velocity.magnitude / _navMeshAgent.speed;
        _animator.SetFloat(Animations.Speed, _speed);

        if (_swordsmanState == SwordsManState.Warrior)
        {
            _coolDownTimer += Time.deltaTime;

            if (_isFirstTimeAttack)
            {
                _timerAfterAttack += Time.deltaTime;
            }
        }
        else if (_swordsmanState == SwordsManState.Worker)
        {
            _coolDownTimer += Time.deltaTime;
            //логика рабочего
        }

        if (_speed > 0.4f)
        {
            _unitChaseCollider.IsMoving = true;
        }
        else if (_speed <= 0.4f)
        {
            _unitChaseCollider.IsMoving = false;
        }
    }
    public void Chase(AIUnit unit)
    {
        if (unit.IsDead == false && _isDead == false && _swordsmanState == SwordsManState.Warrior)
        {
            if (_speed >= 0.1f)
            {
                RotateToTarget(unit.transform);
            }


            if (_coolDownTimer > _attackCoolDown)
            {
                _navMeshAgent.destination = unit.transform.position;
            }

            var distance = (_unit.position - unit.transform.position).magnitude;

            if (distance <= _attackRange && _unitAttackCollider.NumColliders > 0)
            {
                MeleeAttack(unit);
                _isFirstTimeStop = true;
                _isFirstTimeAttack = true;

            }
            else if (_unitAttackCollider.NumColliders == 0 && _navMeshAgent.isStopped && _timerAfterAttack >= 2f)
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
    public void ToAttack(AIUnit unit)
    {
        if (unit.IsDead == false && _isDead == false && _swordsmanState == SwordsManState.Warrior)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = unit.transform.position;

            var distance = (_unit.position - unit.transform.position).magnitude - 1;

            if (distance <= _attackRange)
            {
                MeleeAttack(unit);
                _isFirstTimeStop = true;
                _isFirstTimeAttack = true;

            }
        }

    }
    public void BuildBarrack(BarracksController barrack)
    {
        if (!IsDead && _swordsmanState == SwordsManState.Worker)
        {
            _navMeshAgent.isStopped = false;
            RotateToTarget(barrack.transform);

            // _navMeshAgent.destination = barrack.transform.position;
            if (!barrack.IsCompleted && _resources.Wood >= 20) //|| !barrack.IsCompleted && barrack.IsConstructionInProgress
            {
                _barrackBuild = StartCoroutine(BuildThisBarrack(barrack));
            }
            // else if (!barrack.IsCompleted && barrack.IsPayed)
            // {
            //     _barrackBuild = StartCoroutine(BuildThisBarrack(barrack));
            // }

        }
    }
    public void Conquer(TowerController tower)
    {
        if (!IsDead && _swordsmanState == SwordsManState.Warrior)
        {
            _navMeshAgent.isStopped = false;
            RotateToTarget(tower.transform);

            _navMeshAgent.destination = tower.transform.position;

            _conquerTower = StartCoroutine(ConquerThis(tower));
        }
    }
    public void ExtractWood(WoodController wood)
    {
        if (!IsDead && _swordsmanState == SwordsManState.Worker && wood.WoodCount > 0)
        {
            _navMeshAgent.isStopped = false;
            RotateToTarget(wood.transform);

            _navMeshAgent.destination = wood.transform.position;

            _extractWood = StartCoroutine(ExtractThis(wood));
        }
    }

    void MeleeAttack(AIUnit unit)
    {
        RotateToTarget(unit.transform);

        if (_coolDownTimer > _attackCoolDown)
        {
            Attack();
            _coolDownTimer = 0;
        }
    }
    void WeaponOn()
    {
        _sword.SwordOn();
    }
    void WeaponOff()
    {
        _sword.SwordOff();
    }
    public void ToWarrior()
    {
        _swordsmanState = SwordsManState.Warrior;
        _axe.gameObject.SetActive(false);
        _sword.gameObject.SetActive(true);
        _shield.gameObject.SetActive(true);
        _capsuleCollider.enabled = true;
    }
    public void ToWorker()
    {
        _swordsmanState = SwordsManState.Worker;
        _axe.gameObject.SetActive(true);
        _sword.gameObject.SetActive(false);
        _shield.gameObject.SetActive(false);
        _capsuleCollider.enabled = true;
    }
    public void ToStealth()
    {
        _swordsmanState = SwordsManState.Stealth;
        _axe.gameObject.SetActive(false);
        _sword.gameObject.SetActive(false);
        // _shield.gameObject.SetActive(false);
        _capsuleCollider.enabled = false;
    }
    public void ToStealthFromDiscovered()
    {
        if (_swordsmanState == SwordsManState.Stealth)
        {
            _capsuleCollider.enabled = false;
        }

    }
    public void ToStealthDiscovered()
    {
        if (_swordsmanState == SwordsManState.Stealth)
        {
            _capsuleCollider.enabled = true;
        }
    }
    IEnumerator AddWood(WoodController wood)
    {
        yield return new WaitForSeconds(2.3f);
        wood.ExtractWood(WoodExtract);
    }
    IEnumerator ExtractThis(WoodController wood)
    {
        var distance = (_unit.position - wood.transform.position).magnitude;
        var woodDestination = _navMeshAgent.destination;

        while (distance > _extractRange && wood.WoodCount > 0)
        {
            if (_navMeshAgent.destination != woodDestination)
            {
                StopCoroutine(_extractWood);
            }
            RotateToTarget(wood.transform);
            distance = (_unit.position - wood.transform.position).magnitude;
            yield return null;
        }
        if (distance < _extractRange)
        {
            _navMeshAgent.isStopped = true;
            while (wood != null)
            {
                if (_coolDownTimer > _extractCoolDown && wood.WoodCount > 0)
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
    IEnumerator ConquerThis(TowerController tower)
    {
        var distance = (_unit.position - tower.transform.position).magnitude;
        var towerDestination = _navMeshAgent.destination;
        while (distance > _extractRange)
        {
            if (_navMeshAgent.destination != towerDestination)
            {
                StopCoroutine(_conquerTower);
            }
            RotateToTarget(tower.transform);
            distance = (_unit.position - tower.transform.position).magnitude;
            yield return null;
        }
        if (distance < _extractRange)
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
    IEnumerator BuildThisBarrack(BarracksController barrack)
    {
        var distance = (_unit.position - barrack.transform.position).magnitude;
        var barrackDestination = _navMeshAgent.destination;
        _axe.gameObject.SetActive(false);
        _hammer.gameObject.SetActive(true);

        while (distance > _extractRange + 1)
        {
            if (_navMeshAgent.destination != barrackDestination)
            {
                _axe.gameObject.SetActive(true);
                _hammer.gameObject.SetActive(false);
                StopCoroutine(_barrackBuild);
            }
            RotateToTarget(barrack.transform);
            distance = (_unit.position - barrack.transform.position).magnitude;
            yield return null;
        }
        if (distance < _extractRange + 1)
        {
            _navMeshAgent.isStopped = true;

            barrack.StartBuildBarrack();

            while (!barrack.IsCompleted)
            {
                RotateToTarget(barrack.transform);
                if (_coolDownTimer > _extractCoolDown && !barrack.IsCompleted)
                {
                    _animator.SetTrigger(Animations.WoodHit);
                    _coolDownTimer = 0;
                }
                // else if (barrack.IsConstructionInProgress == false)
                // {
                //     _axe.gameObject.SetActive(true);
                //     _hammer.gameObject.SetActive(false);
                //     StopCoroutine(_barrackBuild);
                //     break;
                // }
                // if (_navMeshAgent.isStopped == false && barrack.IsConstructionInProgress)
                // {
                //     _axe.gameObject.SetActive(true);
                //     _hammer.gameObject.SetActive(false);
                //     barrack.StopBuild();
                //     StopCoroutine(_barrackBuild);
                //     break;
                // }
                yield return null;
            }

        }
        _axe.gameObject.SetActive(true);
        _hammer.gameObject.SetActive(false);
        yield return null;

    }
}
