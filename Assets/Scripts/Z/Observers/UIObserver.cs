using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIObserver : Observer
{
    public void Init(UIManager uIManager,
    UIEventManager uIEventManager,
    ResourceManager resourceManager,
    BuildingsEventManager buildingEventManager, UnitEventManager unitEventManager)
    {
        _uIManager = uIManager;
        _uIEventManager = uIEventManager;
        _resourceManager = resourceManager;
        _buildingEventManager = buildingEventManager;
        _unitEventManager = unitEventManager;

        _uIManager.WorkerButton.onClick.AddListener(WorkerClick);
        _uIManager.WarriorButton.onClick.AddListener(WarriorClick);
        _uIManager.StealthButton.onClick.AddListener(StealthClick);

        _unitEventManager.OnWoodChop += AddWood;

        _buildingEventManager.OnUnitIsNear += OrderUnitOn;
        _buildingEventManager.OnUnitLost += OrderUnitOff;

        _uIEventManager.OnUnitOrder += OrderUnit;
        _uIEventManager.OnCancelUnitOrder += CancelOrderUnit;
        _uIEventManager.OnPayForBarracks += PayForBarracks;
        _uIEventManager.OnReturnPayForBarrack += ReturnPayForBarracks;

        _buildingEventManager.OnGoldMining += AddMiningGold;

        WarriorClick();
        UpdateUIResources();
    }
    void WarriorClick()
    {
        _uIManager.WorkerButton.image.color = Color.white;
        _uIManager.StealthButton.image.color = Color.white;
        _uIManager.WarriorButton.image.color = Color.green;

        _uIEventManager.SwitchToWarrior();
    }
    void WorkerClick()
    {
        _uIManager.WorkerButton.image.color = Color.green;
        _uIManager.WarriorButton.image.color = Color.white;
        _uIManager.StealthButton.image.color = Color.white;

        _uIEventManager.SwitchToWorker();
    }

    void StealthClick()
    {
        _uIManager.WorkerButton.image.color = Color.white;
        _uIManager.WarriorButton.image.color = Color.white;
        _uIManager.StealthButton.image.color = Color.green;

        _uIEventManager.SwitchToStealth();
    }

    void AddWood(WoodController wood, int woodChopAtOnce)
    {
        if (wood.WoodCount > 0)
        {
            _resourceManager.AddWood(woodChopAtOnce);
            _resourceManager.AllWoods.FirstOrDefault(x => x == wood).ExtractWood(woodChopAtOnce);
            _uIManager.WoodText.text = "Wood: " + _resourceManager.Wood;
        }

    }
    public void OrderUnitOn(BarracksController barracks)
    {
        _uIManager.OrderUnitController.gameObject.SetActive(true);
        _uIManager.OrderUnitController.OrderUnitIn(barracks);
    }
    public void OrderUnitOff()
    {
        _uIManager.OrderUnitController.UnitCancel();
        _uIManager.OrderUnitController.gameObject.SetActive(false);
    }
    public void OrderUnit()
    {
        if (_resourceManager.Gold >= 10)
        {
            _uIManager.OrderUnitController.OrderUnitInProgress();
            _resourceManager.RemoveGold(10);
            UpdateUIResources();
        }

    }
    public void CancelOrderUnit()
    {
        _resourceManager.AddGold(10);
        UpdateUIResources();
    }
    public void UpdateUIResources()
    {
        _uIManager.GoldText.text = "Gold: " + _resourceManager.Gold;
        _uIManager.WoodText.text = "Wood: " + _resourceManager.Wood;
    }
    public void PayForBarracks()
    {
        _resourceManager.RemoveWood(20);
        UpdateUIResources();
    }
    public void ReturnPayForBarracks()
    {
        _resourceManager.AddWood(20);
        UpdateUIResources();
    }
    private void AddMiningGold()
    {
        _resourceManager.AddGold(1);
        UpdateUIResources();
    }
}
