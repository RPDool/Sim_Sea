using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer sr;

    public void SetColor(Color color)
    {
        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        sr.color = color;
    }
}
