using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour, ITickable
{
	[Zenject.Inject] private SoundHandler _soundHandler;
	[Zenject.Inject] private TickController _tickController;

    [SerializeField] private ItemsConfig config;
	[SerializeField] private SpawnerItem spawner;
	[SerializeField] private ArchItemsController archItemsController;
	[SerializeField] private ItemCollideGoodStrategy collideGoodStrategy;
	[SerializeField] private ItemCollideBadStrategy collideBadStrategy;

	private List<GoodItem> _goodItems = new();

	public event Action<ICollidableItem> ItemCollideAction;
	public event Action OnDestroyAction;

	public ItemsConfig Config => config;

	private void Awake() 
	{
		spawner.ItemSpawned += OnSpawnItem;
		_tickController.Subscribe(this);
	}

	private void Start() 
	{
		foreach (var item in archItemsController.Items)
		{
			item.CollidedAction += OnCollideItem;
		}
	}

	private void OnDestroy() 
	{
		OnDestroyAction?.Invoke();
		spawner.ItemSpawned -= OnSpawnItem;	
		_tickController.Unsubscribe(this);
	}

	public void Tick() 
	{
		foreach (var item in _goodItems)
		{
			item.transform.GetChild(0).Rotate(new Vector3(0, config.goodItemsData.rotationSpeed * Time.deltaTime, 0));
		}
	}

	private void OnSpawnItem(ICollidableItem item) 
	{
		item.CollidedAction += OnCollideItem;

		if (item is GoodItem goodItem) 
		{
			OnSpawnGoodItem(goodItem);
		}
		else if (item is BadItem badItem)
		{
			OnSpawnBadItem(badItem);
		}

		if (item is AbstractItem abstractItem) 
		{
			if (abstractItem is not IItemCollectable collectableItem) return;

			switch(collectableItem) 
			{
				case GoodItem: abstractItem.Initialize(config.goodItemsData.itemValue); break;
				case BadItem: abstractItem.Initialize(config.badItemsData.itemValue); break;
			}
		}
	}

	private void OnSpawnGoodItem(GoodItem item) 
	{
		_goodItems.Add(item);
	}

	private void OnSpawnBadItem(BadItem item) 
	{

	}

	private void OnCollideItem(ICollidableItem item) 
	{
		Debug.Log("Item collided: " + item);

		item.CollidedAction -= OnCollideItem;
		AbstractItem itemGO = item as AbstractItem;

		ItemCollideAction?.Invoke(item);
		Debug.Log("ItemCollided action");

		if (item is IGoodItem goodItem) 
		{
			OnCollideGoodItem(goodItem);
		}
		else if (item is IBadItem badItem) 
		{
			OnCollideBadItem(badItem);
		}

		Destroy(itemGO.gameObject);
	}

	private void OnCollideGoodItem(IGoodItem item) 
	{
		_soundHandler.InvokeOneClip(config.goodItemsData.soundOnPickup);

		if (_goodItems.Contains(item) && item is GoodItem goodItem)
			_goodItems.Remove(goodItem);

		collideGoodStrategy.Handle();
	}

	private void OnCollideBadItem(IBadItem item) 
	{
		_soundHandler.InvokeOneClip(config.badItemsData.soundOnPickup);

		collideBadStrategy.Handle();	
	}
}
