using Core.ECSGameView.UnityComponentsBridge;
using Core.ECSlogic.Interfaces.Services;
using UnityEngine;
using Zenject;

namespace Core.ECSGameView.Services
{
    public class MapElementsFactory : MonoBehaviour, IMapElementsFactory
    {
        private DiContainer container;
        private IElementStaticService elementStaticService;
        private ISpriteService spriteService;

        [SerializeField] private Transform elementsHolder;
        [SerializeField] private WorldStorage worldStorage;

        private readonly PlayerElementsFactory playerFactory = new();
        private readonly CollectableElementsFactory collectableFactory = new();

        public bool IsInited => playerFactory.IsInited && collectableFactory.IsInited;

        [Inject]
        private void Constructor(DiContainer container, IElementStaticService elementStaticService,
        ISpriteService spriteService)
        {
            this.container = container;
            this.elementStaticService = elementStaticService;
            this.spriteService = spriteService;
        }

        public void Init()
        {
            var factoryInit = new FactoryInit(elementsHolder, worldStorage, container);
            playerFactory.Init(factoryInit);
            collectableFactory.Init(factoryInit);

            var servicesInit = new FactoryInitServices(elementStaticService,spriteService);
            collectableFactory.SetServices(servicesInit);


            playerFactory.CreatePool();
            collectableFactory.CreatePool();
        }


        public IMapView GetElementFromPool(int id)
        {
            return collectableFactory.GetElementFromPool(id);
        }

        public IMapView GetPlayerFromPool()
        {
            return playerFactory.GetPlayerFromPool();
        }
    }
}