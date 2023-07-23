using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IElementStaticService
{
    int[] GetAllElementIds();
    bool GetElementModel(int id, out ElementModel elementModel);
}

public class ElementStaticService : IElementStaticService
{
    private readonly Dictionary<int, ElementModel> elementModels;
    private int[] allElementIds;

    public ElementStaticService()
    {
        //Could be loaded from DB or etc
        elementModels = new Dictionary<int, ElementModel>
        {
            {
                0,
                ElementsStaticDB.zeroElement
            },
            {
                1,
                ElementsStaticDB.firstElement
            },
            {
                2,
                ElementsStaticDB.secondElement
            }
        };


        allElementIds = elementModels.Keys.ToArray();
    }

    public bool GetElementModel(int id, out ElementModel elementModel)
    {
        var result = elementModels.TryGetValue(id, out elementModel);
        if (!result)
        {
            Debug.LogError($"Element {id} not found");
        }
        return result;
    }

    public int[] GetAllElementIds() => allElementIds;
}