using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public GameObject Character;

    public Sprite Sprite;

    private SpriteRenderer _spriteRenderer;

    void OnMouseDown()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = Character.GetComponent<SpriteRenderer>();
        }

        _spriteRenderer.sprite = Sprite;
    }
}
