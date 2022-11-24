using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] UnitEventManager _unitEventManager;
    [SerializeField] BuildingsEventManager _buildingsEventManager;
    [SerializeField] InputEventManager _inputEventManager;
    [SerializeField] UIEventManager _uIEventManager;
    [SerializeField] InputObserver _inputObserver;
    [SerializeField] UnitsObserver _unitObserver;
    [SerializeField] UIObserver _uIObserver;
    [SerializeField] BuildingsObserver _buildingsObserver;
    [SerializeField] BuildingManager _buildingManager;
    [SerializeField] ResourceManager _resourceManager;
    [SerializeField] UIManager _uIManager;
    [SerializeField] UnitsManager _unitsManager;
    [SerializeField] AIUnitManager _aIUnitManager;
    [SerializeField] MissionManager _missionManager;
    [SerializeField] CursorController _cursorController;
    [SerializeField] InputController _inputController;

    private void Awake()
    {
        _buildingManager.Init(_buildingsEventManager, _uIEventManager);
        _resourceManager.Init();
        _uIManager.Init(_uIEventManager, _unitEventManager);

        _aIUnitManager.Init();
        _missionManager.Init();
        _unitsManager.Init(_unitEventManager, _buildingsEventManager);

        _inputController.Init(_inputEventManager);

        _inputObserver.Init(_inputEventManager, _unitsManager, _cursorController, _unitObserver);
        _uIObserver.Init(_uIManager, _uIEventManager, _resourceManager, _buildingsEventManager, _unitEventManager);
        _unitObserver.Init(_inputEventManager, _unitEventManager, _unitsManager, _uIEventManager, _buildingsEventManager);
        _buildingsObserver.Init(_buildingsEventManager, _unitEventManager, _resourceManager, _uIEventManager);
    }

}
