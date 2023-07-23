using System;
using System.Collections.Generic;
using System.Linq;
using Core.ECSlogic.Interfaces.Services;

public class RandomService : IRandomService
{
    private readonly Random random = new Random(321);

    public void Init()
    {
        //Could be Random init by special seed
    }

    public Random GetRandom() => random;

    public bool TryProc(float chance)
    {
        var value = random.Next(0, 100);
        return value <= chance;
    }

    public T GetRandomElement<T>(IList<T> list) => list[RandomElementIndex(list)];
    public int RandomElementIndex<T>(IList<T> list) => random.Next(list.Count);

    public T GetRandomElementWithExcluded<T>(IList<T> collection, IList<T> excluded)
    {
        var filteredCollection = collection.Except(excluded).ToList();

        int randomIndex = RandomElementIndex(filteredCollection);
        return filteredCollection[randomIndex];
    }
}