using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CollectableWorldElement : BaseWorldElement
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void SetSprite(Sprite sprite) => spriteRenderer.sprite = sprite;
}
