using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "ElementsWorldStorage", menuName = "ScriptableObject/Storage/ElementsWorldStorage")]
public class ElementsWorldStorage : ScriptableObject
{
    [SerializeField] private AssetReferenceSprite[] sprites;
    [SerializeField] private AssetReference elementRef;
    public AssetReference GetAssetReference => elementRef;

    public AssetReference GetSpriteRef(int id)
    {
        return sprites[id];
    }

    public AssetReferenceSprite[] GetAllSprites() => sprites;
}
