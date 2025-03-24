using UnityEngine;

public class IndustrialZone : MonoBehaviour
{
    private void Start()
    {
        // Notify the StatManager that an industrial zone has been added
        StatManager.Instance.AddIndustrialZone();
    }

    private void OnDestroy()
    {
        // Notify the StatManager that this industrial zone has been removed
        StatManager.Instance.RemoveIndustrialZone();
    }
}
