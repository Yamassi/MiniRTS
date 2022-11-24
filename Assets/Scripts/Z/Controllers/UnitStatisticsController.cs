using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatisticsController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _extractRange;
    [SerializeField] private float _attackPower;
    [SerializeField] private float _attackCoolDown;
    [SerializeField] private float _extractCoolDown;
    [SerializeField] private int _woodChopAtOnce;

    public float MaxHealth { get => _maxHealth; }
    public float AttackRange { get => _attackRange; }
    public float ExtractRange { get => _extractRange; }
    public float AttackPower { get => _attackPower; }
    public float AttackCoolDown { get => _attackCoolDown; }
    public int WoodChopAtOnce { get => _woodChopAtOnce; }
    public float ExtractCoolDown { get => _extractCoolDown; }

}
