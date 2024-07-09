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
		moneyCounter.OnStart(_itemController);
		sliderBehaviour.OnStart(_itemController);
	}

	private void OnGameEnd() 
	{
		moneyCounter.OnEnd(_itemController);
		sliderBehaviour.OnEnd(_itemController);

		_itemController.ItemCollideAction -= OnGameStarted;

		_itemController.OnDestroyAction -= OnGameEnd;
	}
}

public interface IGameStartable 
{
	void OnStart(ItemController ItemController);
	void OnEnd(ItemController ItemController);
}
