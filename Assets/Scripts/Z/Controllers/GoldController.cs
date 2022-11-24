using UnityEngine;

public class GoldController : MonoBehaviour
{
    public delegate void GoldExtract(int gold);
    public event GoldExtract OnGoldExtract;

    public void ExtractGold(int gold)
    {
        OnGoldExtract?.Invoke(gold);
    }
}
