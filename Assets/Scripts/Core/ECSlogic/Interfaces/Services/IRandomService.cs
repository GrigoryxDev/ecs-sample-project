using System;
using System.Collections.Generic;

namespace Core.ECSlogic.Interfaces.Services
{
    public interface IRandomService
    {
        Random GetRandom();
        bool TryProc(float chance);
        T GetRandomElement<T>(IList<T> collection);
        T GetRandomElementWithExcluded<T>(IList<T> collection, IList<T> excluded);
    }
}