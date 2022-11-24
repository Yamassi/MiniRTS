using UnityEngine;

public class FPSTarget : MonoBehaviour
{

    [SerializeField] int target;

    void Awake()
    {
        Application.targetFrameRate = target;
    }

    void Update()
    {
        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;
    }
}
