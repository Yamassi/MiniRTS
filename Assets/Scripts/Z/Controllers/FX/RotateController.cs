using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 vectorRotation;
    void Update()
    {
        transform.Rotate(vectorRotation * speed * Time.deltaTime);
    }


}
