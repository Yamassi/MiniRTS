using System.Collections.Generic;
using UnityEngine;

public class UnitsAttackColliderController : MonoBehaviour
{
    [SerializeField] List<Unit> _enemies;
    public List<Unit> Enemies { get => _enemies; set => _enemies = value; }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && other.CompareTag("Enemies"))
        {
            if (!_enemies.Contains(unit))
            {
                _enemies.Add(unit);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Unit unit) && other.CompareTag("Enemies"))
        {
            _enemies.Remove(unit);
        }
    }
}
