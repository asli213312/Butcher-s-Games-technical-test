using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMediator : MonoBehaviour
{	
	[Zenject.Inject] private ItemController _itemController;

	[SerializeField] private MoneyCounter moneyCounter;
	[SerializeField] private SliderBehaviour sliderBehaviour;

	private void Start() 
	{
		_itemController.ItemCollideAction += OnGameStarted;
		_itemController.OnDestroyAction += OnGameEnd;
	}

	private void OnGameStarted(ICollidableItem item) 
	{
		
	}

	private void OnGameEnd() 
	{
		_itemController.ItemCollideAction -= OnGameStarted;
		_itemController.OnDestroyAction -= OnGameEnd;
	}
}
