using UnityEngine;

public class House : MonoBehaviour
{
    private void Start()
    {
        StatManager.Instance.AddHouseCapacity(1);
    }
}