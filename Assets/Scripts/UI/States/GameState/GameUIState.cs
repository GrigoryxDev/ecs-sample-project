using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameUIState : BaseState
{
    private IEndGameService endGameService;


    [SerializeField] private Button endGameButton;
    [SerializeField] private CollectedPanelUi collectedPanelUi;
    [SerializeField] private QuestUiPanel questUiPanel;

    private QuestUiUpdater questUiUpdater;

    public override GameStates GetState => GameStates.Game;


    [Inject]
    private void Constructor(IReadOnlyGameModel readOnlyGameModel,
    IEndGameService endGameService, ISpriteService spriteService)
    {
        this.endGameService = endGameService;  

        questUiUpdater = new QuestUiUpdater(spriteService, readOnlyGameModel,questUiPanel);
    }

    private void Start()
    {
        endGameButton.onClick.AddListener(endGameService.End);

        collectedPanelUi.Init();
        questUiUpdater.StartObserve();
    }
}

