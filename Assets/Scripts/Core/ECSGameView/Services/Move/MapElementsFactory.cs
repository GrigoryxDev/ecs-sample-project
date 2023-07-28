using Core.ECSGameView.UnityComponents;
using Core.ECSlogic.Interfaces.Services;
using UnityEngine;

namespace Core.ECSGameView.Services
{
    public class MapElementsFactory : MonoBehaviour, IMapElementsFactory
    {
        [SerializeField] private Transform elementsHolder;

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public IMapView GetElementFromPool(int id)
        {
            throw new System.NotImplementedException();
        }

        public IMapView GetPlayerFromPool()
        {
            throw new System.NotImplementedException();
        }


    }
}