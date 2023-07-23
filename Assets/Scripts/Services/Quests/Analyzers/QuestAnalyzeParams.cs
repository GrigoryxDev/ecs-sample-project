//An abstract class is better suited than an interface for a future extension
public class QuestAnalyzeParams
{
    public int ElementID { get; }
    public int IncreaseAmount { get; }


    public QuestAnalyzeParams(int elementID, int increaseAmount)
    {
        IncreaseAmount = increaseAmount;
        ElementID = elementID;
    }
}