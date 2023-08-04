using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class UIHolder : MonoBehaviour
{
    private IReadOnlyGameModel gameModel;
    [SerializeField] private StateSwitcher stateSwitcher;

    [Inject]
    private void Constructor(IReadOnlyGameModel gameModel)
    {
        this.gameModel = gameModel;
    }

    public void Init()
    {
        gameModel.GetState
        .ObserveEveryValueChanged(x => x.Value)
        .DistinctUntilChanged()
        .Subscribe(newState => stateSwitcher.ChangeState(newState))
        .AddTo(this);
    }
}
