using System.Collections.Generic;
using Core.ECSlogic.Components;
using Core.ECSlogic.Extensions;
using Core.ECSlogic.Interfaces.Services;
using Core.ECSlogic.Models;
using Leopotam.EcsLite;

namespace Core.ECSlogic.Services
{
    public class MapElementsService
    {
        private readonly IRandomService randomService;

        private readonly EcsFilter mapElementsFilter;
        private readonly EcsFilter mapCollectableElementsFilter;

        private readonly EcsWorld ecsWorld;
        private readonly EcsPool<MapElement> mapElements;
        private readonly EcsPool<CreateRandomElement> createRandomElementPool;

        public MapElementsService(EcsWorld world, InitLogicServices initLogic)
        {
            randomService = initLogic.RandomService;

            ecsWorld = world;
            mapElements = world.GetPool<MapElement>();
            createRandomElementPool = world.GetPool<CreateRandomElement>();
            mapElementsFilter = world.Filter<MapElement>().Exc<ExpiredTag>().End();
            mapCollectableElementsFilter = world.Filter<MapElement>().Inc<CollectableElement>().Exc<ExpiredTag>().End();
        }

        public List<Vector2Int> GetElementsOnMap()
        {
            var result = new List<Vector2Int>();
            foreach (var entity in mapElementsFilter)
            {
                var mapElement = mapElements.Get(entity);
                result.Add(mapElement.GridPosition);
            }
            return result;
        }

        public List<Vector2Int> GetCollectablesOnMap()
        {
            var result = new List<Vector2Int>();
            foreach (var entity in mapCollectableElementsFilter)
            {
                var mapElement = mapElements.Get(entity);
                result.Add(mapElement.GridPosition);
            }
            return result;
        }

        public void CreateRndElementsRequest()
        {
            var newRndElement = ecsWorld.NewEntity();
            ref var createRndElement = ref createRandomElementPool.Add(newRndElement);
            createRndElement.Count = randomService.GetRandom().Next(1, 3);
        }
    }
}