public class CollectQuestAnalyzer : BaseQuestAnalyzer
{
    public override void Analyze<T>(T quest, QuestAnalyzeParams questParams)
    {
        var collectQuest = quest as CollectElementQuest;
        var collectParams = questParams as CollectElementParams;

        if (collectParams.ElementID == collectQuest.CollectElementId)
        {
            collectQuest.SetCollectCount(collectQuest.GetCurrentCount.Value + collectParams.IncreaseAmount);
        }
    }
}