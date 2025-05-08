using UnityEngine;

public class CommercialZone : MonoBehaviour
{
    private void Start()
    {
        StatManager.Instance.AddCommercialZone();
    }

    private void OnDestroy()
    {
        StatManager.Instance.RemoveCommercialZone();
    }
}
