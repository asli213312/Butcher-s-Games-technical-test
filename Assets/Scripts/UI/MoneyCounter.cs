using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : AbstractCounter, IObserver<GameStateHandler>
{
	[Zenject.Inject] private SliderBehaviour _sliderBehaviour;
	[Zenject.Inject] private PlayerController _playerController;
	[Zenject.Inject] private GameStateHandler _gameStateHandler;
	[Zenject.Inject] private ItemController _itemController;

	protected override void Start() 
	{
		base.Start();

		_playerController.Mover.CurrentInputType.MoveStarted += OnGameStarted;
		_itemController.ItemCollideAction += UpdateCounter;

		Initialize(Mathf.RoundToInt(_sliderBehaviour.Slider.value));
	}

	protected override void OnDestroy() 
	{
		base.OnDestroy();

		_playerController.Mover.CurrentInputType.MoveStarted -= OnGameStarted;
		_itemController.ItemCollideAction -= UpdateCounter;
	}

	public void OnNotify<GameStateHandler>() 
	{
		counterText.gameObject.SetActive(false);
	}

	private void OnGameStarted() 
	{
		counterText.gameObject.SetActive(true);
	}

	private void UpdateCounter(ICollidableItem item) 
	{
		AbstractItem abstractItem = item as AbstractItem;

		if (abstractItem is IItemCollectable collectableItem) 
		{
			InvokeCount(collectableItem.ItemValue);
		}
	}
}
