using UnityEngine;

public class CommercialZone : MonoBehaviour
{
    private void Start()
    {
        // Notify the StatManager that a commercial zone has been added
        StatManager.Instance.AddCommercialZone();
    }

    private void OnDestroy()
    {
        // Notify the StatManager that this commercial zone has been removed
        StatManager.Instance.RemoveCommercialZone();
    }
}
