using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateHandler : MonoBehaviour, IObservable<GameStateHandler>
{
	[Zenject.Inject] private SliderBehaviour _sliderBehaviour;
	[Zenject.Inject] private PlayerController _playerController;

	[SerializeField] private GameWinState winState;
	[SerializeField] private GameFailState failState;
	[SerializeField] private NextState[] states;

	[SerializeField] private UnityEvent onGameStarted;

	public event Action FailAction;
	public event Action WinAction;
	public GameWinState WinState => winState;	
	public GameFailState FailState => failState;

	private readonly List<IObserver<GameStateHandler>> _observers = new();

	private void Start() 
	{
		_sliderBehaviour.Slider.onValueChanged.AddListener(OnSliderValueChanged);
		_playerController.Mover.CurrentInputType.MoveStarted += () => onGameStarted?.Invoke();
	}

	private void OnDestroy() 
	{
		_sliderBehaviour.Slider.onValueChanged.RemoveListener(OnSliderValueChanged);
		_playerController.Mover.CurrentInputType.MoveStarted -= () => onGameStarted?.Invoke();
	}

	public void Subscribe(IObserver<GameStateHandler> observer) 
	{
		if (!_observers.Contains(observer)) 
		{
			_observers.Add(observer);
		}
	}

	public void Unsubscribe(IObserver<GameStateHandler> observer) 
	{
		if (observer == null) return;

		if (_observers.Count <= 0) return;

		if (_observers.Contains(observer)) 
		{
			_observers.Remove(observer);
		}
	}

	public void Notify() 
	{
		foreach (var item in _observers)
		{
			item.OnNotify<GameStateHandler>();
		}
	}

	private void OnSliderValueChanged(float value)  
	{
		if (value <= 0) 
		{
			failState.Handle();
			FailAction?.Invoke();
			Notify();
		}
	}
}
