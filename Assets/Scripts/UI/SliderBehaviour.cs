using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderBehaviour : MonoBehaviour, IGameStartable
{
	[Zenject.Inject] private PlayerController _playerController;

	[SerializeField] private SliderBehaviourData data;
	[SerializeField] private TextMeshProUGUI statusText;
	[SerializeField] private Slider slider;
	[SerializeField] private Image fillImage;

	public event Action<SliderBehaviourState> StateChanged;

	private ItemController _itemController;
	private SliderBehaviourState _currentState;

	private void Start() 
	{
		_playerController.Mover.CurrentInputType.MoveStarted += OnMoveStarted;

		_currentState = data.states[1];
		slider.maxValue = data.states[^1].stateRange.y;
		slider.value = data.initialValue;

		OnNewState();
	}

	private void OnDestroy() 
	{
		_playerController.Mover.CurrentInputType.MoveStarted -= OnMoveStarted;
	}

	public void OnStart(ItemController itemController) 
	{
		_itemController = itemController;
		_itemController.ItemCollideAction += OnCollideItem;
	}

	public void OnEnd(ItemController itemController) 
	{
		_itemController.ItemCollideAction -= OnCollideItem;
	}

	private void OnMoveStarted() 
	{
		slider.gameObject.SetActive(true);
	}

    private void OnCollideItem(ICollidableItem item) 
    {
    	if (item is GoodItem) 
    	{
    		slider.value += _currentState.stepValue;
    	}
    	else if (item is BadItem) 
    	{
    		slider.value -= _currentState.stepValue;
    	}

    	if (slider.value <= 0) 
    	{
    		_currentState = data.states[0];
    		OnNewState();
    		return;
    	}

    	SliderBehaviourState state = SelectState();

    	if (_currentState != state) 
    	{
    		_currentState = state;
    		OnNewState();
    	}
    }

    private SliderBehaviourState SelectState() 
    {
    	if (_currentState == data.states[^1]) return data.states[^1];

    	foreach (var state in data.states) 
		{
			if (slider.value <= state.stateRange.y && slider.value >= state.stateRange.x) 
			{
				return state;
			}
		}

		Debug.LogError("Can't find slider state for value: " + slider.value);
		return null;
    }

    private void OnNewState() 
    {
    	statusText.text = _currentState.stateText;
    	fillImage.sprite = _currentState.fillSprite;

    	StateChanged?.Invoke(_currentState);
    }
}
