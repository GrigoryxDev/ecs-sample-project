using Core.ECSGameView.UnityComponentsBridge;
using Core.ECSGameView.Services;

namespace Core.ECSlogic.Interfaces.Services
{
    public interface IMapElementsFactory : IInitService
    {
        IMapView GetPlayerFromPool();
        IMapView GetElementFromPool(int id);
    }
}