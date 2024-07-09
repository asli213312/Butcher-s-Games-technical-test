using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : AbstractCounter, IGameStartable
{
	private ItemController _itemController;

	public void OnStart(ItemController itemController) 
	{
		_itemController = itemController;
		_itemController.ItemCollideAction += UpdateCounter;	
	}

	public void OnEnd(ItemController itemController) 
	{
		_itemController.ItemCollideAction -= UpdateCounter;	
	}

	private void UpdateCounter(ICollidableItem item) 
	{
		if (item is not GoodItem goodItem) return;
		
		if (goodItem.IsMoney) 
		{
			InvokeCount();
		}
	}
}
