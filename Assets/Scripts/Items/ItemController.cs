using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	[SerializeField] private SpawnerItem spawner;
	[SerializeField] private ItemCollideGoodStrategy collideGoodStrategy;
	[SerializeField] private ItemCollideBadStrategy collideBadStrategy;

	public event Action<ICollidableItem> ItemCollideAction;
	public event Action OnDestroyAction;

	private void Awake() 
	{
		spawner.ItemSpawned += OnSpawnItem;
	}

	private void OnDestroy() 
	{
		OnDestroyAction?.Invoke();
		spawner.ItemSpawned -= OnSpawnItem;	
	}

	private void OnSpawnItem(ICollidableItem item) 
	{
		item.CollidedAction += OnCollideItem;
	}

	private void OnCollideItem(ICollidableItem item) 
	{
		Debug.Log("Item collided: " + item);

		item.CollidedAction -= OnCollideItem;
		AbstractItem itemGO = item as AbstractItem;

		if (item is GoodItem goodItem) 
		{
			OnCollideGoodItem(goodItem);
		}
		else if (item is BadItem badItem) 
		{
			OnCollideBadItem(badItem);
		}

		ItemCollideAction?.Invoke(item);

		Destroy(itemGO.gameObject);
	}

	private void OnCollideGoodItem(GoodItem item) 
	{
		collideGoodStrategy.Handle();
	}

	private void OnCollideBadItem(BadItem item) 
	{
		collideBadStrategy.Handle();	
	}
}
