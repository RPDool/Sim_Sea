using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color originalColor;
    public Color hoverColor = Color.yellow; // Set this in the Inspector

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
        originalColor = color; // Update original color when set
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