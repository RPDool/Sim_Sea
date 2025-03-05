using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f;
    public GameObject cellPrefab;
    public Color color1 = Color.white;
    public Color color2 = Color.gray;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
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

    void CreateCell(Vector2 position, Color cellColor)
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
}
