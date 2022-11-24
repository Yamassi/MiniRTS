using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] private SwordController _sword;
    [SerializeField] private ShieldController _shield;
    [SerializeField] private AxeController _axe;
    [SerializeField] private HammerController _hammer;
    private UnitStatisticsController _statisticsController;

    public SwordController Sword { get => _sword; set => _sword = value; }
    public ShieldController Shield { get => _shield; set => _shield = value; }
    public AxeController Axe { get => _axe; set => _axe = value; }
    public HammerController Hammer { get => _hammer; set => _hammer = value; }

    public void Init(UnitStatisticsController statisticsController)
    {
        _statisticsController = statisticsController;
        _sword.Init(_statisticsController.AttackPower);
    }

    public void SwitchToWarrior()
    {
        _sword.gameObject.SetActive(true);
        _shield.gameObject.SetActive(true);
        _axe.gameObject.SetActive(false);
        _hammer.gameObject.SetActive(false);
    }
    public void SwitchToChopWorker()
    {
        _sword.gameObject.SetActive(false);
        _shield.gameObject.SetActive(false);
        _axe.gameObject.SetActive(true);
        _hammer.gameObject.SetActive(false);
    }
    public void SwitchToConstructWorker()
    {
        _sword.gameObject.SetActive(false);
        _shield.gameObject.SetActive(false);
        _axe.gameObject.SetActive(false);
        _hammer.gameObject.SetActive(true);
    }
    public void SwitchToStealth()
    {
        _sword.gameObject.SetActive(false);
        _shield.gameObject.SetActive(false);
        _axe.gameObject.SetActive(false);
        _hammer.gameObject.SetActive(false);
    }

    public void WeaponOn()
    {
        _sword.SwordOn();
    }
    public void WeaponOff()
    {
        _sword.SwordOff();
    }
}

