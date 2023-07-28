
using Core.ECSGameView.Services;
using Core.ECSlogic.Interfaces.Services;
using UnityEngine;

namespace Core.ECSGameView.Models
{
    [System.Serializable]
    public class InitView
    {
        [SerializeField] private Grid grid;
        [SerializeField] private MapElementsFactory mapElementsFactory;

        public Grid GetGrid => grid;
        public IMapElementsFactory GetMapElementsFactory => mapElementsFactory;
    }
}
