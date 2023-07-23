public class CollectQuestAnalyzer : BaseQuestAnalyzer
{
    public override void Analyze<T>(T quest, QuestAnalyzeParams questParams)
    {
        var collectQuest = quest as CollectElementQuest;
        collectQuest.SetCollectCount(collectQuest.GetCurrentCount.Value + questParams.IncreaseAmount);
        //TODO: update quest on completed
    }
}