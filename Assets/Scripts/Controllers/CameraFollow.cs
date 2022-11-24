using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject flag;
    void Update()
    {
        transform.position = new Vector3(flag.transform.position.x + 9, flag.transform.position.y + 22, flag.transform.position.z - 30);
    }

}
