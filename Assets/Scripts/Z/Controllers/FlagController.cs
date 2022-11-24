using UnityEngine;

public class FlagController : MonoBehaviour
{
    UnitController _flagHandler;
    public void UpdateFlagHandler(UnitController flagHandler)
    {
        _flagHandler = flagHandler;
        transform.parent = _flagHandler.transform;
    }
}
