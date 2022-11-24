using UnityEngine;
using System;

[CreateAssetMenu]
public class BuildingsEventManager : ScriptableObject
{
    public delegate void UnitIsNear(BarracksController barracks);
    public event UnitIsNear OnUnitIsNear;
    public void UnitIsNearBarrack(BarracksController barracks) => OnUnitIsNear?.Invoke(barracks);

    public Action OnUnitLost;
    public void UnitLostFromBarrack() => OnUnitLost?.Invoke();

    public Action OnGoldMining;
    public void GoldMining() => OnGoldMining?.Invoke();

    public Action OnTowerConquered;
    public void TowerConquered() => OnTowerConquered?.Invoke();
}
