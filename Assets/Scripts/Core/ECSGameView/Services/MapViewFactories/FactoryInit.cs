using UnityEngine;
using Zenject;

namespace Core.ECSGameView.Services
{
    public struct FactoryInit
    {
        public Transform elementsHolder;
        public WorldStorage worldStorage;
        public DiContainer container;

        public FactoryInit(Transform elementsHolder, WorldStorage worldStorage, DiContainer container)
        {
            this.elementsHolder = elementsHolder;
            this.worldStorage = worldStorage;
            this.container = container;
        }
    }
}