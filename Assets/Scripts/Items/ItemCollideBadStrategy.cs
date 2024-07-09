using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCollideBadStrategy : ItemCollideStrategy
{
	protected override ICollidableItem ItemType { get => _badItem; set => _badItem = value as BadItem; }
	private BadItem _badItem;

	protected override void Invoke() 
	{

	}
}
