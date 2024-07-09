using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCollideGoodStrategy : ItemCollideStrategy
{
	protected override ICollidableItem ItemType { get => _goodItem; set => _goodItem = value as GoodItem; }
	private GoodItem _goodItem;

	protected override void Invoke() 
	{

	}
}
