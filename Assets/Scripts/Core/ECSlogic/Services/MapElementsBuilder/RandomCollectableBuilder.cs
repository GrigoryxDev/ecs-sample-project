using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Interfaces.Services;
using Core.ECSlogic.Models;
using Leopotam.EcsLite;

namespace Core.ECSlogic.Services
{
    public class RandomCollectableBuilder : BaseMapElementsBuilder
    {
        private readonly IRandomService randomService;
        private readonly IElementStaticService elementStaticService;
        private readonly EcsPool<CollectableElement> collectableElements;

        public RandomCollectableBuilder(EcsWorld world, InitLogicServices initServices) : base(world, initServices)
        {
            randomService = initServices.RandomService;
            elementStaticService = initServices.ElementStaticService;
            collectableElements = world.GetPool<CollectableElement>();
        }

        public ref CollectableElement Create(Vector2Int gridPosition)
        {
            var entity = CreateMapElement(gridPosition);
            ref var component = ref collectableElements.Add(entity);

            var allElementIds = elementStaticService.GetAllElementIds();
            var randomId = randomService.GetRandomElement(allElementIds);

            component.ID = randomId;
            component.Count = 1; //Can be increased from special settings or etc

            return ref component;
        }
    }
}