using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIHolder : MonoBehaviour
{
    [SerializeField] private StateSwitcher stateSwitcher;

    public void Init(IReadOnlyGameModel gameModel)
    {
        gameModel.GetState
        .ObserveEveryValueChanged(x => x.Value)
        .DistinctUntilChanged()
        .Subscribe(newState => stateSwitcher.ChangeState(newState)).AddTo(this);
    }
}
