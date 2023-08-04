public class CollectElementParams : QuestAnalyzeParams
{
    public int ElementID { get; }
    public int IncreaseAmount { get; }


    public CollectElementParams(int elementID, int increaseAmount)
    {
        IncreaseAmount = increaseAmount;
        ElementID = elementID;
    }
}