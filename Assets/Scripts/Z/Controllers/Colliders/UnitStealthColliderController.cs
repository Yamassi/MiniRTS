using UnityEngine;
using System;

public class UnitStealthColliderController : MonoBehaviour
{
    public Action OnUnitDiscovered;
    public Action OnUnitHided;

    public void UnitDiscovered()
    {
        OnUnitDiscovered?.Invoke();
    }
    public void UnitHided()
    {
        OnUnitHided?.Invoke();
    }
}
