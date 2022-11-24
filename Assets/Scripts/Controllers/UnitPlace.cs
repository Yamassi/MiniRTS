using UnityEngine;

public class UnitPlace : MonoBehaviour
{
    [SerializeField] private Transform _unitInPlace;
    public Transform UnitInPlace
    {
        get => _unitInPlace;
        set => _unitInPlace = value;
    }
}
