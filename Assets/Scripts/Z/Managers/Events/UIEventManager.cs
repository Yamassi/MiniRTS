using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class UIEventManager : ScriptableObject
{
    public Action OnSwitchToWarrior;
    public void SwitchToWarrior() => OnSwitchToWarrior?.Invoke();
    public Action OnSwitchToWorker;
    public void SwitchToWorker() => OnSwitchToWorker?.Invoke();
    public Action OnSwitchToStealth;
    public void SwitchToStealth() => OnSwitchToStealth?.Invoke();
    public Action OnUnitOrder;
    public void OrderUnit() => OnUnitOrder?.Invoke();
    public Action OnCancelUnitOrder;
    public void CancelOrderUnit() => OnCancelUnitOrder?.Invoke();
    public Action OnPayForBarracks;
    public void PayForBarrack() => OnPayForBarracks?.Invoke();
    public Action OnReturnPayForBarrack;
    public void ReturnPayForBarrack() => OnReturnPayForBarrack?.Invoke();
}
