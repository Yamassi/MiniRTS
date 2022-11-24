using UnityEngine;

public class PopUpTextController : MonoBehaviour
{
    [SerializeField] float _destroyTime;
    Camera _camera;
    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
        Destroy(gameObject, _destroyTime);
    }
    private void Update()
    {
        Quaternion.LookRotation(_camera.transform.position, Vector3.up);
    }
}
