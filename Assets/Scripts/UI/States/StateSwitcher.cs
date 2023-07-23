using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StateSwitcher : MonoBehaviour
{
    [SerializeField] private UIStatesModel[] states;
    private BaseState currentState;

    public void ChangeState(GameStates newState)
    {
        //TODO: test
        var stateModel = states.FirstOrDefault(s => s.State == newState);



    }
}

[Serializable]
public struct UIStatesModel
{
    public AssetReference Reference;
    public GameStates State;
}
