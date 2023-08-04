using System.Collections.Generic;
using UnityEngine;

public class QuestAnalyzerHolder
{
    private readonly Dictionary<QuestType, BaseQuestAnalyzer> questAnalyzers = new()
    {
         {QuestType.Collect, new CollectQuestAnalyzer()},
    };
    private IQuestStorageService questStorageService;

    public QuestAnalyzerHolder(IQuestStorageService questStorageService)
    {
        this.questStorageService = questStorageService;
    }

    public void AnalyzeActiveQuests(List<int> currentQuests, QuestAnalyzeParams questParams)
    {
        foreach (var questId in currentQuests)
        {
            questStorageService.GetQuestModel(questId, out var quest);
            AnalyzeQuest(quest, questParams);
        }
    }

    public void AnalyzeQuest(QuestBaseModel quest, QuestAnalyzeParams questParams)
    {
        if (questAnalyzers.TryGetValue(quest.Type, out var analyzer))
        {
            analyzer.Analyze(quest, questParams);
        }
        else
        {
            Debug.LogError($"Unknown quest type:{quest.Type}");
        }
    }


}