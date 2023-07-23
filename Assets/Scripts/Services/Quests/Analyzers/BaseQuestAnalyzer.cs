//An abstract class is better suited than an interface for a future extension
public abstract class BaseQuestAnalyzer
{
    public abstract void Analyze<T>(T quest, QuestAnalyzeParams questParams) where T : QuestBaseModel;
}