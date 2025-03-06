using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for detecting UI clicks

public class PlacementManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject housePrefab;
    public GameObject piratePortPrefab; // New Pirate Port prefab
    public Button equipRoadButton;
    public Button equipHouseButton;
    public Button equipPiratePortButton; // New button for Pirate Port

    private GameObject selectedPrefab = null;
    public float cellSize = 1f; // Ensure grid snapping works properly
    public LayerMask ignoreLayer; // Ignore grid tile layer
    public float gapSize = 0.05f; // Small gap to ensure colliders don't touch

    private void Start()
    {
        equipRoadButton.onClick.AddListener(() => SelectObject(roadPrefab));
        equipHouseButton.onClick.AddListener(() => SelectObject(housePrefab));
        equipPiratePortButton.onClick.AddListener(() => SelectObject(piratePortPrefab)); // Add listener for Pirate Port
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
        mouseWorldPos.z = 0.5f;  // Set z to a higher value to place the object in front of the UI buttons

        Vector2 gridPos = GetSnappedGridPosition(mouseWorldPos);  // Grid snap position

        if (CanPlaceObject(gridPos))
        {
            Instantiate(selectedPrefab, gridPos, Quaternion.identity);  // Place the object if the position is clear
        }
    }

    private bool CanPlaceObject(Vector2 position)
    {
        // Check if the selected prefab is a house (needs 3x3 space)
        if (selectedPrefab == housePrefab)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(3 * cellSize - gapSize, 3 * cellSize - gapSize), 0, ~ignoreLayer);
            return colliders.Length == 0; // Ensure no overlap
        }
        // If it's a road (needs 1x1 space)
        else if (selectedPrefab == roadPrefab)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(cellSize - gapSize, cellSize - gapSize), 0, ~ignoreLayer);
            return colliders.Length == 0; // Ensure no overlap
        }
        // If it's a pirate port (let's assume it needs 2x2 space)
        else if (selectedPrefab == piratePortPrefab)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(2 * cellSize - gapSize, 2 * cellSize - gapSize), 0, ~ignoreLayer);
            return colliders.Length == 0; // Ensure no overlap
        }

        return true;  // If no object is selected, allow placement
    }

    private Vector2 GetSnappedGridPosition(Vector3 worldPos)
    {
        return new Vector2(
            Mathf.Round(worldPos.x / cellSize) * cellSize,
            Mathf.Round(worldPos.y / cellSize) * cellSize
        );
    }

    // Check if the pointer is over a UI element (to prevent placing objects while clicking UI buttons)
    private bool IsPointerOverUIObject()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // Raycast against the UI
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0; // Return true if any UI element was hit
    }
}
