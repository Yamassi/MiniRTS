using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private UnitChaseColliderController _chaseCollider;
    [SerializeField] private UnitAttackColliderController _attackCollider;
    [SerializeField] private UnitStealthColliderController _stealthCollider;
    private CapsuleCollider _unitCollider;
    public UnitChaseColliderController ChaseCollider { get => _chaseCollider; set => _chaseCollider = value; }
    public UnitAttackColliderController AttackCollider { get => _attackCollider; set => _attackCollider = value; }
    public UnitStealthColliderController StealthCollider { get => _stealthCollider; set => _stealthCollider = value; }
    public CapsuleCollider UnitCollider { get => _unitCollider; set => _unitCollider = value; }

    public void Init()
    {
        _unitCollider = GetComponent<CapsuleCollider>();
    }
}
