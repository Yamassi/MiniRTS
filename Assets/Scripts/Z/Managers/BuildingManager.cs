using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] List<BarracksController> _allBarracks;
    [SerializeField] List<TowerController> _allTowers;
    private BuildingsEventManager _buildingsEventManager;
    private UIEventManager _uIEventManager;
    public List<BarracksController> AllBarracks { get => _allBarracks; }
    public List<TowerController> AllTowers { get => _allTowers; }

    public void Init(BuildingsEventManager buildingsEventManager, UIEventManager uIEventManager)
    {
        _buildingsEventManager = buildingsEventManager;
        _uIEventManager = uIEventManager;
        foreach (BarracksController barracks in _allBarracks)
        {
            barracks.Init(_buildingsEventManager, _uIEventManager);
        }
        foreach (TowerController tower in _allTowers)
        {
            tower.Init(_buildingsEventManager);
        }
    }
    //TODO
    // public void BuildBarrack(BarracksController barracks)
    // {
    //     var barrack = _allBarracks.FirstOrDefault<BarracksController>();
    //     barrack.StartBuildBarrack();
    // }
    // public void StopBuildBarrack(BarracksController barracks)
    // {
    //     var barrack = _allBarracks.FirstOrDefault<BarracksController>();
    //     barrack.StopBuild();
    // }
}
