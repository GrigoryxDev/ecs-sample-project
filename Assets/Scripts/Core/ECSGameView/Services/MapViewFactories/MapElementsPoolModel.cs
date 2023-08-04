using System.Collections.Generic;

namespace Core.ECSGameView.Services
{
    public class MapElementsPoolModel<T> where T : BaseWorldElement
    {
        private readonly Queue<T> pool = new();

        public void AddToPool(T view)
        {
            pool.Enqueue(view);
        }

        public bool TryGetFromPool(out T result)
        {
            return pool.TryDequeue(out result);
        }

        public void ReturnToPool(T view)
        {
            var viewGO = view.GetViewGo();
            viewGO.SetActive(false);

            pool.Enqueue(view);
        }
    }
}