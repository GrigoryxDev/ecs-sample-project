using UniRx;
using UnityEngine;

public interface IElementAnalyzeService
{
    void Collect(int id, int count);
}

public class ElementAnalyzeService : IElementAnalyzeService
{
    private readonly GameModel gameModel;
    private readonly IElementStaticService elementStaticService;

    public ElementAnalyzeService(GameModel gameModel,
    IElementStaticService elementStaticService)
    {
        this.gameModel = gameModel;
        this.elementStaticService = elementStaticService;
    }

    public void Collect(int id, int count)
    {
        if (!elementStaticService.GetElementModel(id, out _))
        {
            return;
        }

        if (!gameModel.CollectedElements.ContainsKey(id))
        {
            gameModel.CollectedElements.Add(id, 0);
        }

        gameModel.CollectedElements[id] += count;
    }
}