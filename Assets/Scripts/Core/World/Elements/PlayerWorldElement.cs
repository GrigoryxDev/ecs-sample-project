using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldElement : BaseWorldElement
{
    [SerializeField] private float defaultSpriteAngle = 90;
    
    public override void UpdateWorldPosition(Vector2 worldPosition)
    {
        var moveDirection = worldPosition - (Vector2)transform.position;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - defaultSpriteAngle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        base.UpdateWorldPosition(worldPosition);
    }
}
