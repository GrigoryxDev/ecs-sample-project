using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class StateSwitcher : MonoBehaviour
{
    private DiContainer container;

    [SerializeField] private UIStatesStorage statesStorage;
    [SerializeField] private Transform root;

    private BaseState currentState;


    [Inject]
    private void Constructor(DiContainer container)
    {
        this.container = container;
    }
    
    public void ChangeState(GameStates newState)
    {
        //TODO: test all states exists
        var states = statesStorage.GetAllStates();
        var stateModel = states.FirstOrDefault(s => s.State == newState);

        if (currentState != null)
        {
            AddressableHelper.ReleaseInstance(currentState.gameObject);
        }

        AddressableHelper.LoadGOAssetAsyncAndForget<BaseState>(stateModel.Reference,
      (state) =>
        {
            currentState = container.InstantiatePrefabForComponent<BaseState>(state, root);
        });
    }
}