using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class AbstractItem : MonoBehaviour, ICollidableItem, IItemCollectable, IItem
{
    [SerializeField] private ItemsData data;

    public ItemsData Data => data;

    public event Action<ICollidableItem> CollidedAction;
    public int ItemValue => Value;
    protected int Value { get; private set; }

    public void Initialize(int itemValue) 
    {
        Value = itemValue;
    }

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

public interface IItem 
{
    ItemsData Data { get; }
}

public interface IBadItem : IItem
{

}

public interface IGoodItem : IItem
{
    bool IsMoney { get; }
}

public interface IItemCollectable
{
    int ItemValue { get; }
}
