using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemFactory
{
	public ICollidableItem Item { get => ItemType; }
	protected abstract ICollidableItem ItemType { get; set; }
	protected ItemFactoryData Data;
	protected MonoBehaviour MonoBehaviour;

	public ItemFactory(ItemFactoryData data, MonoBehaviour monoBehaviour) 
	{
		Data = data;
		ItemType = data.ItemType;
		MonoBehaviour = monoBehaviour;

		Debug.Log("Factory ItemType: " + ItemType);
		Debug.Log("Factory Item: " + Item);
	}

    public ICollidableItem Create(Vector3 position, Quaternion rotation) 
    {
    	try 
	    {
	        if (ItemType != Data.ItemType)
	        {
	            throw new InvalidOperationException("Factory type not correct");
	        }

	        return CreateItem(position, rotation);
	    }
	    catch (InvalidOperationException e) 
	    {
	        Debug.LogError(e.Message);
	        return null;
	    }
    }

    protected abstract ICollidableItem CreateItem(Vector3 position, Quaternion rotation);
}

public class GoodItemFactory : ItemFactory
{
	protected override ICollidableItem ItemType { get => _itemType; set => _itemType = value as GoodItem; }
	private GoodItem _itemType;

	public GoodItemFactory(ItemFactoryData data, MonoBehaviour monoBehaviour) : base(data, monoBehaviour) 
	{

	}

	protected override ICollidableItem CreateItem(Vector3 position, Quaternion rotation) 
	{
		GoodItem item = MonoBehaviour.Instantiate(_itemType, position, rotation);
		return item;
	}
}

public class BadItemFactory : ItemFactory
{
	protected override ICollidableItem ItemType { get => _itemType; set => _itemType = value as BadItem; }
	private BadItem _itemType;


	public BadItemFactory(ItemFactoryData data, MonoBehaviour monoBehaviour) : base(data, monoBehaviour) 
	{

	}

	protected override ICollidableItem CreateItem(Vector3 position, Quaternion rotation) 
	{
		BadItem item = MonoBehaviour.Instantiate(_itemType, position, rotation);
		return item; 
	}
}

public class ItemFactoryData 
{
	public ItemsData ItemData;
	public ICollidableItem ItemType;

	public ItemFactoryData(ICollidableItem itemType, ItemsData itemData) 
	{
		ItemType = itemType;
		ItemData = itemData;
	}
}
