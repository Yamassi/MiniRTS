using UnityEngine;

public class WardController : MonoBehaviour
{
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.TryGetComponent<StealthCollider>(out StealthCollider stealthCollider))
    //     {
    //         stealthCollider.OnUnitDiscovered();
    //         print("Юнит обнаружен");
    //     }
    // }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<UnitStealthColliderController>(out UnitStealthColliderController stealthCollider))
        {
            stealthCollider.OnUnitDiscovered();
            print("Юнит обнаружен");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<UnitStealthColliderController>(out UnitStealthColliderController stealthCollider))
        {
            stealthCollider.OnUnitHided();
            print("Юнит скрылся");
        }
    }
}
