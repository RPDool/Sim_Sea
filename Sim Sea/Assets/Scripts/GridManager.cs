using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f;
    public GameObject cellPrefab;
    public Color color1 = Color.white;
    public Color color2 = Color.gray;
    public Camera mainCamera;
    public float cameraMoveSpeed = 0.1f;

    private Vector3 lastMousePosition;
    private bool isDragging = false;

    private void Start()
    {
        GenerateGrid();
        CenterCamera();
    }

    private void Update()
    {
        HandleCameraMovement();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 position = new Vector2(x * cellSize, y * cellSize);
                CreateCell(position, (x + y) % 2 == 0 ? color1 : color2);
            }
        }
    }

    private void CreateCell(Vector2 position, Color cellColor)
    {
        if (cellPrefab != null)
        {
            GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
            Tile tile = cell.GetComponent<Tile>();
            if (tile != null)
            {
                tile.SetColor(cellColor);
            }
        }
    }

    private void CenterCamera()
    {
        if (mainCamera != null)
        {
            float centerX = (gridWidth - 1) * cellSize / 2f;
            float centerY = (gridHeight - 1) * cellSize / 2f;
            mainCamera.transform.position = new Vector3(centerX, centerY, mainCamera.transform.position.z);
        }
    }

    private void HandleCameraMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = mainCamera.ScreenToWorldPoint(lastMousePosition) - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mainCamera.transform.position += delta;
            lastMousePosition = Input.mousePosition;
        }
    }
}
