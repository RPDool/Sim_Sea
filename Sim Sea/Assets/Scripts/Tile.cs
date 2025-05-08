using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    public Color hoverColor = Color.yellow;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            originalColor = sr.color;
        }
    }

    public void SetColor(Color color)
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        sr.color = color;
        originalColor = color;
    }

    private void OnMouseEnter()
    {
        if (sr != null)
        {
            sr.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (sr != null)
        {
            sr.color = originalColor;
        }
    }
}