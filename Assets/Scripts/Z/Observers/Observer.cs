using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    protected UnitEventManager _unitEventManager;
    protected BuildingsEventManager _buildingEventManager;
    protected InputEventManager _inputEventManager;
    protected UIEventManager _uIEventManager;
    protected InputObserver _inputObserver;
    protected UnitsObserver _unitObserver;
    protected BuildingManager _buildingManager;
    protected ResourceManager _resourceManager;
    protected UIManager _uIManager;
    protected UnitsManager _unitsManager;
    protected AIUnitManager _aIUnitManager;
    protected MissionManager _missionManager;
    protected FlagHandler _flagManager;
    protected CursorController _cursorController;
    protected InputController _inputController;
}