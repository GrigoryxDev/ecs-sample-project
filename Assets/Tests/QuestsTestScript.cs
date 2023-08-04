using NUnit.Framework;

public class QuestsTestScript
{
    private IQuestStorageService questStorageService;
    private QuestAnalyzerHolder analyzerHolder;

    [SetUp]
    public void Setup()
    {
        questStorageService = new QuestStorageService();
        analyzerHolder = new QuestAnalyzerHolder(questStorageService);
    }

    [Test]
    public void TestQuestProgress()
    {
        //Arrange test

        //All quests in static are collect, need update for future
        var allQuestsIds = questStorageService.GetAllQuestIds();
        var firstQuestID = allQuestsIds[0];
        questStorageService.GetQuestModel(firstQuestID, out var firstQuestModel);
        var collectQuest = firstQuestModel as CollectElementQuest;

        //Act test
        var collectQuestParams = new CollectElementParams(collectQuest.CollectElementId, 1);
        analyzerHolder.AnalyzeQuest(firstQuestModel, collectQuestParams);

        collectQuestParams = new CollectElementParams(collectQuest.CollectElementId + 1, 1);
        analyzerHolder.AnalyzeQuest(firstQuestModel, collectQuestParams);

        collectQuestParams = new CollectElementParams(collectQuest.CollectElementId, 2);
        analyzerHolder.AnalyzeQuest(firstQuestModel, collectQuestParams);

        //Assert test
        Assert.AreEqual(3, collectQuest.GetCurrentCount.Value);
    }

    [Test]
    public void TestQuestStorageNotEmpty()
    {
        //Arrange test
        var allQuestsIds = questStorageService.GetAllQuestIds();
        bool result = false;

        //Act test
        result = allQuestsIds.Length > 0;

        //Assert test
        Assert.AreEqual(true, result);
    }
}
