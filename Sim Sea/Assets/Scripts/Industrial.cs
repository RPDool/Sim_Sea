using UnityEngine;

public class IndustrialZone : MonoBehaviour
{
    private void Start()
    {
        StatManager.Instance.AddIndustrialZone();
    }

    private void OnDestroy()
    {
        StatManager.Instance.RemoveIndustrialZone();
    }
}
