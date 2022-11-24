using UnityEngine;

public class UnitsCheckColliderController : MonoBehaviour
{
    [SerializeField] int _numUnitsCheckCollider;
    [SerializeField] float _unitsCheckColliderRadius;
    Collider[] _unitsCheckColliders = new Collider[1];
    [SerializeField] LayerMask _layerMask;
    bool _isProtected;
    public bool IsProtected { get => _isProtected; }

    private void Update()
    {
        _numUnitsCheckCollider = Physics.OverlapSphereNonAlloc(transform.position, _unitsCheckColliderRadius, _unitsCheckColliders, _layerMask);
        if (_numUnitsCheckCollider > 0)
        {
            _isProtected = true;
        }
        else if (_numUnitsCheckCollider == 0)
        {
            _isProtected = false;
        }
    }
}
