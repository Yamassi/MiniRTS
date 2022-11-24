using UnityEngine;
using System;

public class BarracksColliderController : MonoBehaviour
{
    [SerializeField] int _numBarracksCollider;
    [SerializeField] float _barracksColliderRadius;
    Collider[] _barracksColliders = new Collider[1];
    [SerializeField] LayerMask _layerMask;
    private BarracksController _barracks;
    private BuildingsEventManager _buildingsEventManager;
    private bool isNearFound = false;
    private bool isNearLost = false;
    private UnitsManager _unitsManager;
    public UnitsManager UnitsManager { get => _unitsManager; }

    public void Init(BuildingsEventManager buildingsEventManager, BarracksController barracks)
    {
        _buildingsEventManager = buildingsEventManager;
        _barracks = barracks;
    }

    private void Update()
    {
        _numBarracksCollider = Physics.OverlapSphereNonAlloc(transform.position, _barracksColliderRadius, _barracksColliders, _layerMask);
        if (_numBarracksCollider > 0)
        {
            if (!isNearFound)
            {
                _buildingsEventManager.UnitIsNearBarrack(_barracks);
                isNearFound = true;
                isNearLost = false;

            }
        }

        else if (_numBarracksCollider == 0)
        {
            if (!isNearLost)
            {
                _buildingsEventManager.UnitLostFromBarrack();
                isNearLost = true;
                isNearFound = false;
            }
        }
    }
}
