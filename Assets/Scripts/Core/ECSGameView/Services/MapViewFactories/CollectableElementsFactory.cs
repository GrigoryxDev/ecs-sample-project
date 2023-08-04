using System.Collections.Generic;
using Core.ECSGameView.UnityComponentsBridge;
using UnityEngine;

namespace Core.ECSGameView.Services
{
    public class CollectableElementsFactory : BaseFactory
    {
        private readonly MapElementsPoolModel<CollectableWorldElement> elementsPool = new();


        private IElementStaticService elementStaticService;
        private ISpriteService spriteService;

        public bool IsElementsInited { get; private set; }

        private void TrySetInitStatus()
        {
            IsInited = IsElementsInited;
        }

        public override void SetServices(FactoryInitServices factoryInitServices)
        {
            elementStaticService = factoryInitServices.ElementStaticService;
            spriteService = factoryInitServices.SpriteService;

            base.SetServices(factoryInitServices);
        }

        public override void CreatePool()
        {
            CreateElements();
        }

        private void CreateElements()
        {
            var playerGORef = worldStorage.ElementsWorldStorage.GetAssetReference;
            AddressableHelper.LoadGOAssetAsyncAndForget<CollectableWorldElement>(playerGORef, (go) =>
            {
                var allElements = elementStaticService.GetAllElementIds();
                foreach (var item in allElements)
                {
                    InitElement(go, elementsPool);
                }

                IsElementsInited = true;
                TrySetInitStatus();
            });
        }

        public IMapView GetElementFromPool(int id)
        {
            var isElementExists = elementsPool.TryGetFromPool(out var worldElement);
            if (!isElementExists)
            {
                var elementRef = worldStorage.ElementsWorldStorage.GetAssetReference;
                worldElement = AddressableHelper.GetGoAsset<CollectableWorldElement>(elementRef);

                InitElement(worldElement, elementsPool);
            }


            if (spriteService.TryGetSprite(id, out var sprite))
            {
                worldElement.SetSprite(sprite);
            }
            else
            {
                Debug.Log($"Sprite not found for id: {id}");
            }
            
            worldElement.GetViewGo().SetActive(true);
            return worldElement;
        }
    }
}