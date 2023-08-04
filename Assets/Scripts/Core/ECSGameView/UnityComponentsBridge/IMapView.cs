using UnityEngine;

namespace Core.ECSGameView.UnityComponentsBridge
{
    public interface IMapView
    {
        void UpdateWorldPosition(Vector2 worldPosition);
        void SendDestroy();
        GameObject GetViewGo();
    }
}