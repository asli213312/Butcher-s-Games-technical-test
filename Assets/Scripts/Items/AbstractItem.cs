using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItem : MonoBehaviour, ICollidableItem
{
    public event Action<ICollidableItem> CollidedAction;

	private void OnTriggerEnter(Collider col) 
    {
        CollidedAction?.Invoke(this);
        OnCollide();
    }

    protected abstract void OnCollide();
}

public interface ICollidableItem 
{
    event Action<ICollidableItem> CollidedAction;
}
