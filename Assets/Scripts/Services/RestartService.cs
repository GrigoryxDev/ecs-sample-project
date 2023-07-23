
public interface IRestartService
{
    void Restart();
}

public class RestartService : IRestartService
{
    private readonly GameModel gameModel;
    private readonly IQuestDynamicService questDynamicService;

    public RestartService(GameModel gameModel, IQuestDynamicService questDynamicService)
    {
        this.gameModel = gameModel;
        this.questDynamicService = questDynamicService;
    }

    public void Restart()
    {
        gameModel.State.Value = GameStates.Game;
        gameModel.CollectedElements.Clear();
        questDynamicService.GetNextQuest();
    }
}