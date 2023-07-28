using UnityEngine;
using UnityEngine.UI;

public class EndUIState : BaseState
{
    [SerializeField] private Button restartButton;

    public override GameStates GetState => GameStates.End;

    private void Start()
    {
        
    }
}

