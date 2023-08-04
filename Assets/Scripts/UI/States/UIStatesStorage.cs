using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "UIStatesStorage", menuName = "ScriptableObject/Storage/UIStatesStorage")]

public class UIStatesStorage : ScriptableObject
{
    [SerializeField] private UIStatesModel[] states;

    public UIStatesModel[] GetAllStates() => states;
}


[Serializable]
public struct UIStatesModel
{
    public AssetReference Reference;
    public GameStates State;
}