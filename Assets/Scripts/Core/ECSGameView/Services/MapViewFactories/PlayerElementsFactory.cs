using Core.ECSGameView.UnityComponentsBridge;

namespace Core.ECSGameView.Services
{
    public class PlayerElementsFactory : BaseFactory
    {
        private readonly MapElementsPoolModel<PlayerWorldElement> playerPool = new();

        public override void CreatePool()
        {
            var playerGORef = worldStorage.PlayerWorldStorage.GetAssetReference;
            AddressableHelper.LoadGOAssetAsyncAndForget<PlayerWorldElement>(playerGORef, (go) =>
            {
                for (int i = 0; i < 2; i++)
                {
                    InitElement(go, playerPool);
                }
                IsInited = true;
            });
        }

        public IMapView GetPlayerFromPool()
        {
            var isPlayerExists = playerPool.TryGetFromPool(out var player);
            if (!isPlayerExists)
            {
                var playerGORef = worldStorage.PlayerWorldStorage.GetAssetReference;
                player = AddressableHelper.GetGoAsset<PlayerWorldElement>(playerGORef);

                InitElement(player, playerPool);
            }

            player.GetViewGo().SetActive(true);
            return player;
        }
    }
}