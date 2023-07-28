using UnityEngine;

namespace Core.ECSGameView.UnityComponents
{
    public interface IMapView
    {
        public void UpdateWorldPosition(Vector2 worldPosition);
        public void SendDestroy();

    }
}