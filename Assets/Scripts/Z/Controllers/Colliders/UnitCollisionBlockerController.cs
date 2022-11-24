using UnityEngine;

public class UnitCollisionBlockerController : MonoBehaviour
{
    [SerializeField] CapsuleCollider _unitCollider;
    [SerializeField] CapsuleCollider _unitCollisionBlocker;
    private void Awake()
    {
        Physics.IgnoreCollision(_unitCollider, _unitCollisionBlocker, true);
    }
}
