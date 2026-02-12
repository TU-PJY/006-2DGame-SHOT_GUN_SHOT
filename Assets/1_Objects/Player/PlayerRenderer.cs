using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    void Awake()
    {
        spriteRenderer.sprite = sprite;
    }
}
