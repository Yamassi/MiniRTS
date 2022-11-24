using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class SwordController : MonoBehaviour
{
    [SerializeField] protected bool _isEnemiesWeapon;
    protected BoxCollider _boxCollider;
    protected float _weaponPower;
    public void Init(float weaponPower)
    {
        _weaponPower = weaponPower;
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (other.CompareTag("Units") && _isEnemiesWeapon)
            {
                damageable.TakeDamage(_weaponPower);
            }
            else if (other.CompareTag("Enemies") && !_isEnemiesWeapon)
            {
                damageable.TakeDamage(_weaponPower);
            }
        }

    }
    public void SwordOn()
    {
        _boxCollider.enabled = true;
    }
    public void SwordOff()
    {
        _boxCollider.enabled = false;
    }
}
