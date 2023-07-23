using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IQuestStorageService
{
    bool GetQuestModel(int id, out QuestBaseModel elementModel);
    int[] GetAllQuestIds();
}

public class QuestStorageService : IQuestStorageService
{
    private readonly Dictionary<int, QuestBaseModel> elementModels;
    private int[] allElementIds;

    public QuestStorageService()
    {
        //Could be loaded from DB or etc
        elementModels = new Dictionary<int, QuestBaseModel>
        {
            {
                0,
                QuestsStaticDB.zeroElement
            },
            {
                1,
                QuestsStaticDB.firstElement
            }
        };


        allElementIds = elementModels.Keys.ToArray();
    }

    public bool GetQuestModel(int id, out QuestBaseModel elementModel)
    {
        var result = elementModels.TryGetValue(id, out elementModel);
        if (!result)
        {
            Debug.LogError($"Element {id} not found");
        }
        return result;
    }

    public int[] GetAllQuestIds() => allElementIds;
}

