using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerItem : MonoBehaviour
{	
	[SerializeField, SerializeReference] private AbstractItem[] items;	
	[SerializeField] private Transform[] itemPatterns;

	public event Action<ICollidableItem> ItemSpawned;

	private List<ItemFactory> _factories = new();

	private void Start() 
	{
		CreateFactories();
		Debug.Log("Created factories count: " + _factories.Count);

		foreach (var itemPattern in itemPatterns) 
		{
			for (var i = 0; i < itemPattern.childCount; i++) 
			{
				AbstractItem item = itemPattern.GetChild(i).GetComponent<AbstractItem>();
				ICollidableItem createdItem = SpawnItem(item, item.transform.position, item.transform.rotation);
				ItemSpawned?.Invoke(createdItem);
			}
		}
	}

	private ICollidableItem SpawnItem(ICollidableItem itemType, Vector3 position, Quaternion rotation) 
	{
		foreach (var factory in _factories) 
		{
			Debug.Log("Factory itemType in spawnItem: " + factory.Item);
			if (factory.Item.GetType() != itemType.GetType()) continue;
			return factory.Create(position, rotation);
		}

		return null;
	}

	private void CreateFactories() 
	{
		foreach (var item in items) 
		{
			if (_factories.Count == items.Length) break;

			if (_factories.Count > 0) 
			{
				if (_factories.Exists(x => x.Item.GetType() == item.GetType())) continue; 
			}

			if (item is GoodItem) 
			{
				_factories.Add(new GoodItemFactory(new ItemFactoryData(item), this));
			}
			else if (item is BadItem) 
			{
				_factories.Add(new BadItemFactory(new ItemFactoryData(item), this));
			}
		}
	}
}
