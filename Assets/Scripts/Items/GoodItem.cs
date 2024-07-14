using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodItem : AbstractItem, IGoodItem
{
	[SerializeField] private bool isMoney;

	public bool IsMoney => isMoney;

    protected override void OnCollide() 
    {
    	
    }
}
