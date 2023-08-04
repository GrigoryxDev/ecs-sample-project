using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "PlayerWorldStorage", menuName = "ScriptableObject/Storage/PlayerWorldStorage")]
public class PlayerWorldStorage : ScriptableObject
{
    [SerializeField] private AssetReference elementRef;
    public AssetReference GetAssetReference => elementRef;
}