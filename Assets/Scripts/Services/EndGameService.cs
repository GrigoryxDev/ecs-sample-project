public interface IEndGameService
{
    void End();
}

public class EndGameService : IEndGameService
{
    private readonly GameModel gameModel;


    public EndGameService(GameModel gameModel)
    {
        this.gameModel = gameModel;

    }

    public void End()
    {
        gameModel.State.Value = GameStates.End;

    }
}