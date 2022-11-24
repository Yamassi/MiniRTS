using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int _gold;
    [SerializeField] private int _wood;
    [SerializeField] private List<WoodController> _allWoods;
    [SerializeField] private List<TowerController> _allTowers;
    public int Gold { get => _gold; set => _gold = value; }
    public int Wood { get => _wood; set => _wood = value; }
    public List<WoodController> AllWoods { get => _allWoods; set => _allWoods = value; }
    public List<TowerController> AllTowers { get => _allTowers; set => _allTowers = value; }
    public void Init()
    {

    }
    public void AddGold(int gold)
    {
        _gold += gold;
    }
    public void AddWood(int wood)
    {
        _wood += wood;
    }
    public void RemoveGold(int gold)
    {
        _gold -= gold;
    }
    public void RemoveWood(int wood)
    {
        _wood -= wood;
    }
}
