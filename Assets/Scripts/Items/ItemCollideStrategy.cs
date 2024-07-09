using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ItemCollideStrategy : MonoBehaviour
{
	[SerializeField, SerializeReference] private AbstractItem item;

	public ICollidableItem Item { get => ItemType; }
	protected abstract ICollidableItem ItemType { get; set; }

	private void Start() 
	{
		ItemType = item;
	}

	public void Handle() 
	{
		Invoke();
	}

	protected abstract void Invoke();
}
