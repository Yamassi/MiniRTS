using UnityEngine;

public class UnitChaseColliderController : MonoBehaviour
{
    public delegate void UnitChase(UnitSystem unit);
    public event UnitChase OnUnitChase;
    public delegate void AIUnitChase(AIUnit aIUnit);
    public event AIUnitChase OnAIUnitChase;
    [SerializeField] bool isEnemy;
    bool isMoving = false;
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out AIUnit aIunit) && other.CompareTag("Enemies") && !isEnemy && !isMoving)
        {
            if (!aIunit.IsDead)
            {
                OnAIUnitChase?.Invoke(aIunit);
            }

        }
        if (other.TryGetComponent(out UnitSystem unit) && other.CompareTag("Units") && isEnemy)
        {
            if (!unit.IsDead)
            {

                OnUnitChase?.Invoke(unit);
            }
        }
    }
}
