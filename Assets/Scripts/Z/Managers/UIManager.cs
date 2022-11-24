using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _woodText;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private Button _workerButton;
    [SerializeField] private Button _warriorButton;
    [SerializeField] private Button _stealthButton;
    [SerializeField] private OrderUnitController _orderUnitController;
    public TextMeshProUGUI WoodText { get => _woodText; set => _woodText = value; }
    public TextMeshProUGUI GoldText { get => _goldText; set => _goldText = value; }
    public Button WorkerButton { get => _workerButton; set => _workerButton = value; }
    public Button WarriorButton { get => _warriorButton; set => _warriorButton = value; }
    public Button StealthButton { get => _stealthButton; set => _stealthButton = value; }
    public OrderUnitController OrderUnitController { get => _orderUnitController; set => _orderUnitController = value; }
    public void Init(UIEventManager uIEventManager, UnitEventManager unitEventManager)
    {
        _orderUnitController.Init(uIEventManager, unitEventManager);
    }
}
