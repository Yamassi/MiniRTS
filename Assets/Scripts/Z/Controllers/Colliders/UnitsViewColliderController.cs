using UnityEngine;

public class UnitsViewColliderController : MonoBehaviour
{
    public delegate void SeeTheEnemy(Transform enemyTransform);
    public event SeeTheEnemy OnSeeTheEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && other.CompareTag("Enemies"))
        {
            OnSeeTheEnemy?.Invoke(unit.transform);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && other.CompareTag("Enemies"))
        {
            OnSeeTheEnemy?.Invoke(unit.transform);
        }
    }
}
