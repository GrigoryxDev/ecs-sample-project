using UniRx;
using UnityEngine;

public interface IElementAnalyzeService
{
    void Collect(int id, int count);
}

public class ElementAnalyzeService : IElementAnalyzeService
{
    private readonly GameModel gameModel;
    private readonly IQuestDynamicService questDynamicService;
    private readonly IElementStaticService elementStaticService;

    public ElementAnalyzeService(GameModel gameModel, IQuestDynamicService questDynamicService,
    IElementStaticService elementStaticService)
    {
        this.gameModel = gameModel;
        this.questDynamicService = questDynamicService;
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