using System;

public abstract class QuestBaseModel
{
    public int QuestID { get; }
    public QuestType Type { get; }

    public QuestBaseModel(int QuestId, QuestType QuestType)
    {
        QuestID = QuestId;
        Type = QuestType;
    }

    public abstract void ResetQuest();
    public abstract bool IsCompleted();
}