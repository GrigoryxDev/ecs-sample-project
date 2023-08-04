
using System.Collections.Generic;
using UniRx;
using Core.ECSlogic.Interfaces.Services;

public interface IQuestDynamicService
{
    QuestBaseModel GetNextQuest();
}

public class QuestDynamicService : IQuestDynamicService
{
    private readonly IQuestStorageService questStorageService;
    private readonly GameModel gameModel;
    private readonly IRandomService randomService;

    private readonly QuestAnalyzerHolder analyzerHolder;

    public QuestDynamicService(IQuestStorageService questStorageService, GameModel gameModel, IRandomService randomService)
    {
        this.questStorageService = questStorageService;
        this.gameModel = gameModel;
        this.randomService = randomService;

        analyzerHolder = new QuestAnalyzerHolder(questStorageService);

        gameModel.GetCollectedElements.ObserveAdd().Subscribe(
        added =>
        {
            var questParams = new CollectElementParams(added.Key, added.Value);
            analyzerHolder.AnalyzeActiveQuests(GetCurrentQuests(), questParams);
        });

        gameModel.GetCollectedElements.ObserveReplace().Subscribe(
        added =>
        {
            var questParams = new CollectElementParams(added.Key, added.NewValue - added.OldValue);
            analyzerHolder.AnalyzeActiveQuests(GetCurrentQuests(), questParams);
        });
    }

    public QuestBaseModel GetNextQuest()
    {
        var currentQuests = GetCurrentQuests();

        var allQuestIds = questStorageService.GetAllQuestIds();
        var nextRndQuestId = randomService.GetRandomElementWithExcluded(allQuestIds, currentQuests);

        questStorageService.GetQuestModel(nextRndQuestId, out var nextQuest);
        nextQuest.ResetQuest();

        gameModel.CurrentQuest.Value = nextQuest;
        return nextQuest;
    }

    private List<int> GetCurrentQuests()
    {
        List<int> currentQuests = new();
        if (gameModel.GetCurrentQuest.Value != null)
        {
            QuestBaseModel currentQuest = gameModel.GetCurrentQuest.Value;
            currentQuests.Add(currentQuest.QuestID);
        }

        return currentQuests;
    }
}