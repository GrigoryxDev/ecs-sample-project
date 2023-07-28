using Core.ECSGameView.UnityComponents;

namespace Core.ECSlogic.Interfaces.Services
{
    public interface IMapElementsFactory : IInitService
    {
        IMapView GetPlayerFromPool();
        IMapView GetElementFromPool(int id);
    }
}