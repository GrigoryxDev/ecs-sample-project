public static class QuestsStaticDB
{
    public static QuestBaseModel zeroElement = new CollectElementQuest(0, QuestType.Collect, ElementsStaticDB.zeroElement.ID, 10);
    public static QuestBaseModel firstElement = new CollectElementQuest(1, QuestType.Collect, ElementsStaticDB.firstElement.ID, 10);
}