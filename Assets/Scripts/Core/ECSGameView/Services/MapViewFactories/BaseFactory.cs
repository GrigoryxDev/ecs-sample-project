using UnityEngine;
using Zenject;

namespace Core.ECSGameView.Services
{
    public abstract class BaseFactory
    {
        protected Transform elementsHolder;
        protected WorldStorage worldStorage;
        protected DiContainer container;

        public bool IsInited { get; protected set; }

        public virtual void Init(FactoryInit factoryInit)
        {
            elementsHolder = factoryInit.elementsHolder;
            worldStorage = factoryInit.worldStorage;
            container = factoryInit.container;
        }

        public virtual void SetServices(FactoryInitServices factoryInitServices)
        {

        }

        public abstract void CreatePool();

        protected virtual void InitElement<T>(T go, MapElementsPoolModel<T> pool) where T : BaseWorldElement
        {
            var element = container.InstantiatePrefabForComponent<T>(go, elementsHolder);
            element.Init((view) => pool.ReturnToPool(element));
            element.gameObject.SetActive(false);
            pool.AddToPool(element);
        }
    }
}