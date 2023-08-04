using Core.ECSlogic.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Core.ECSGameView.Helpers;
using Core.ECSGameView.Models;

namespace Core.ECSGameView.Systems
{
    public class InputObservableSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsPoolInject<InputMoveTarget> inputMoveTargetPool;
        private readonly EcsWorldInject world;
        private readonly EcsCustomInject<InitView> initViewServices;
        private readonly EcsCustomInject<IReadOnlyGameModel> readOnlyGameModel;

        private Grid grid;
        private Camera camera;

        public void Init(IEcsSystems systems)
        {
            grid = initViewServices.Value.GetGrid;
            camera = Camera.main;
        }

        public void Run(IEcsSystems systems)
        {
            if (readOnlyGameModel.Value.GetState.Value == GameStates.Game)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                    Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
                    Vector3Int position = grid.WorldToCell(worldPoint);

                    var entity = world.Value.NewEntity();
                    ref var inputMoveTarget = ref inputMoveTargetPool.Value.Add(entity);

                    inputMoveTarget.GridPosition = ((Vector2Int)position).AdaptToExtensionVector2Int();
                }
            }
        }
    }
}