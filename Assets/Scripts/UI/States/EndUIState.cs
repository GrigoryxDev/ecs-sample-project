using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EndUIState : BaseState
{
    private IRestartService restartService;

    [SerializeField] private Button restartButton;

    public override GameStates GetState => GameStates.End;

    [Inject]
    private void Constructor(IRestartService restartService)
    {
        this.restartService = restartService;
    }

    private void Start()
    {
        restartButton.onClick.AddListener(restartService.Restart);
    }
}

