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
    private readonly Dictionary<int, QuestBaseModel> questsModels;
    private int[] allQuestsIds;

    public QuestStorageService()
    {
        //Could be loaded from DB or etc
        questsModels = new Dictionary<int, QuestBaseModel>
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


        allQuestsIds = questsModels.Keys.ToArray();
    }

    public bool GetQuestModel(int id, out QuestBaseModel elementModel)
    {
        var result = questsModels.TryGetValue(id, out elementModel);
        if (!result)
        {
            Debug.LogError($"Element {id} not found");
        }
        return result;
    }

    public int[] GetAllQuestIds() => allQuestsIds;
}

