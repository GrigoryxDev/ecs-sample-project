using System;
using Core.ECSGameView.UnityComponentsBridge;
using UnityEngine;

public abstract class BaseWorldElement : MonoBehaviour, IMapView
{
    private Action<BaseWorldElement> returnToPool;

    public GameObject GetViewGo()
    {
        return gameObject;
    }

    public void Init(Action<BaseWorldElement> returnToPool)
    {
        this.returnToPool = returnToPool;
    }

    public void SendDestroy()
    {
        returnToPool(this);
    }

    public void SendOnPoolReturn()
    {

    }
   
    public virtual void UpdateWorldPosition(Vector2 worldPosition)
    {
        transform.position = worldPosition;
    }
}
