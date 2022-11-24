using UnityEngine;

public class UnitAttackColliderController : MonoBehaviour

{
    [SerializeField] LayerMask layerMask;
    int numColliders;

    public int NumColliders { get => numColliders; }

    private void Update()
    {
        Collider[] hitColliders = new Collider[1];
        numColliders = Physics.OverlapSphereNonAlloc(transform.position, 2f, hitColliders, layerMask);
    }
}
