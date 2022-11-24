using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsObserver : MonoBehaviour
{
    private BuildingsEventManager _buildingsEventManager;
    private UIEventManager _uIEventManager;
    private UnitEventManager _unitEventManager;
    private ResourceManager _resourceManager;
    public void Init(BuildingsEventManager buildingsEventManager, UnitEventManager unitEventManager, ResourceManager resourceManager, UIEventManager uIEventManager)
    {
        _buildingsEventManager = buildingsEventManager;
        _unitEventManager = unitEventManager;
        _resourceManager = resourceManager;
        _uIEventManager = uIEventManager;

        _unitEventManager.OnBarracksBuild += BuildBarrack;
        _unitEventManager.OnCancelBarracksBuild += CancelBuildBarrack;
    }

    private void BuildBarrack(BarracksController barracks)
    {
        if (!barracks.IsBuildProcess)
        {
            _uIEventManager.PayForBarrack();
        }
        barracks.StartBuildBarrack();
    }

    private void CancelBuildBarrack(BarracksController barracks)
    {
        if (barracks.IsBuildProcess)
        {
            _uIEventManager.ReturnPayForBarrack();
        }
        barracks.StopBuild();
    }
}
