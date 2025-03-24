using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlacementManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject housePrefab;
    public GameObject piratePortPrefab;
    public GameObject shipyardPrefab;

    public Button equipRoadButton;
    public Button equipHouseButton;
    public Button equipPiratePortButton;
    public Button equipShipyardButton; 

    private GameObject selectedPrefab = null;
    public float cellSize = 1f;
    public LayerMask ignoreLayer;
    public float gapSize = 0.05f;

    private StatManager statManager;

    private void Start()
    {
        equipRoadButton.onClick.AddListener(() => SelectObject(roadPrefab));
        equipHouseButton.onClick.AddListener(() => SelectObject(housePrefab));
        equipPiratePortButton.onClick.AddListener(() => SelectObject(piratePortPrefab));
        equipShipyardButton.onClick.AddListener(() => SelectObject(shipyardPrefab));

        statManager = FindFirstObjectByType<StatManager>();
    }

    private void Update()
    {
        if (selectedPrefab != null && Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
        {
            PlaceObject();
        }
    }

    private void SelectObject(GameObject prefab)
    {
        selectedPrefab = (selectedPrefab == prefab) ? null : prefab;
    }

    private void PlaceObject()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0.5f;

        Vector2 gridPos = GetSnappedGridPosition(mouseWorldPos);

        if (CanPlaceObject(gridPos))
        {
            Instantiate(selectedPrefab, gridPos, Quaternion.identity);
        }
    }

    private bool CanPlaceObject(Vector2 position)
    {
        float sizeX = 1, sizeY = 1;

        if (selectedPrefab == housePrefab)
        {
            sizeX = sizeY = 3;
        }
        else if (selectedPrefab == piratePortPrefab || selectedPrefab == shipyardPrefab)
        {
            sizeX = sizeY = 2;
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(sizeX * cellSize - gapSize, sizeY * cellSize - gapSize), 0, ~ignoreLayer);
        return colliders.Length == 0;
    }

    private Vector2 GetSnappedGridPosition(Vector3 worldPos)
    {
        return new Vector2(
            Mathf.Round(worldPos.x / cellSize) * cellSize,
            Mathf.Round(worldPos.y / cellSize) * cellSize
        );
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }
}
