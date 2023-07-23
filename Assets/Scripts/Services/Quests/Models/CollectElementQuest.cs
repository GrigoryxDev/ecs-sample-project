using UniRx;

public class CollectElementQuest : QuestBaseModel
{
    public int CollectElementId { get; }
    public int NeedCount { get; }

    private readonly ReactiveProperty<int> currentCount = new();
    public IReadOnlyReactiveProperty<int> GetCurrentCount => currentCount;

    public CollectElementQuest(int QuestID, QuestType QuestType,
    int collectElementId, int needCount) : base(QuestID, QuestType)
    {
        CollectElementId = collectElementId;
        NeedCount = needCount;
    }

    public void SetCollectCount(int count) => currentCount.Value = count;

    public override void ResetQuest()
    {
        currentCount.Value = 0;
    }

    public override bool IsCompleted()
    {
        return GetCurrentCount.Value >= NeedCount;
    }
}