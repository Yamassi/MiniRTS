using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrderUnitController : MonoBehaviour
{
    [SerializeField] private BarracksController _barracks;
    private Image _coolDown;
    private UIEventManager _uIEventManager;
    private UnitEventManager _unitEventManager;
    private Coroutine _unitProgress;
    public void Init(UIEventManager uIEventManager, UnitEventManager unitEventManager)
    {
        _coolDown = GetComponentInChildren<CoolDownController>().gameObject.GetComponent<Image>();
        _uIEventManager = uIEventManager;
        _unitEventManager = unitEventManager;
    }
    public void OrderUnitIn(BarracksController barracks)
    {
        _barracks = barracks;
    }
    public void UnitOrder()
    {
        //Кружится CoolDown
        _uIEventManager.OrderUnit();
    }
    public void UnitCancel()
    {
        _coolDown.fillAmount = 0;
        _uIEventManager.CancelOrderUnit();
    }
    void UnitIsDone()
    {
        Vector3 instantiatePositionOffset = new Vector3(0, 0, -2);
        var instantiatePosition = _barracks.transform.position + instantiatePositionOffset;

        // TODO Доделать
        _unitEventManager.AddUnit(instantiatePosition);
    }
    public void OrderUnitInProgress()
    {
        _unitProgress = StartCoroutine(UnitInProgress());
    }
    IEnumerator UnitInProgress()
    {
        while (_coolDown.fillAmount < 1)
        {
            _coolDown.fillAmount += Time.deltaTime / 5;
            yield return null;
        }
        if (_coolDown.fillAmount >= 1)
        {
            UnitIsDone();
            _coolDown.fillAmount = 0;
            yield return null;
        }
        yield return null;
    }
}
