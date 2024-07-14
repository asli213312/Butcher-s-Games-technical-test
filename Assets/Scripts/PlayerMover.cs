using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IObserver<GameStateHandler>
{
    [Zenject.Inject] private IObservable<GameStateHandler> _gameStateHandler;

	[SerializeField] private bool isTouchInput;
	[SerializeField] private float smoothSpeedSwipe;
	[SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 bounds;
    [SerializeField] private Vector2 boundsOnZ;
	[SerializeField] private MoveStrategy moveStrategy;
	[SerializeField] private Transform moveObject;

	public IInput CurrentInputType { get; private set; }
    public Transform PlayerObj => moveObject;
	private PlayerMoveStrategy _currentMoveStrategy;

	private bool _isMoving;

    public void Initialize()
    {
        CurrentInputType = isTouchInput ? new TouchInput(new InputData(
                                            smoothSpeedSwipe, bounds, new Vector3(1, 0, 0)), moveObject)
                                         : new MouseInput(new InputData(
                                            smoothSpeedSwipe, bounds, new Vector3(1, 0, 0)), moveObject);

		SelectMoveStrategy();

		CurrentInputType.MoveStarted += OnMoveStarted;

        _gameStateHandler.Subscribe(this);
    }

    public void Handle()
    {
        CurrentInputType.Handle();

        if (_isMoving)
        	_currentMoveStrategy.Handle();
    }
    
    public void ChangeDirection(Vector3 direction) 
    {
        Vector3 selectedDirection = Vector3.forward;

        if (direction.x > 0) 
        {
            selectedDirection = new Vector3(moveObject.position.x, 0, 0);
        }
        else if (direction.y > 0) 
        {
            selectedDirection = new Vector3(0, moveObject.position.y, 0);
        }
        else  if (direction.z > 0)
        {
            selectedDirection = new Vector3(0, 0, moveObject.position.z);
        }

        CurrentInputType.InputData = new InputData(smoothSpeedSwipe, boundsOnZ, selectedDirection);
    }

    public void SelectMoveStrategy(PlayerMoveStrategy selectedStrategy) 
    {
        _currentMoveStrategy = selectedStrategy;
    }

    private void SelectMoveStrategy() 
    {
        PlayerMoveStrategy selectedStrategy = null;

    	switch(moveStrategy) 
    	{
    		case MoveStrategy.Linear: selectedStrategy = new PlayerMoveLinear(moveObject, moveSpeed, new Vector3(0, 0, 1));
                break;
    		default: return;
    	}

        _currentMoveStrategy = selectedStrategy;
    }

    public void OnNotify<GameStateHandler>() 
    {
        _isMoving = false;
        CurrentInputType.IsActive = false;
    }

    private void OnMoveStarted() 
    {
    	_isMoving = true;
        CurrentInputType.IsActive = true;
    }

    private void OnDestroy() 
    {
        _gameStateHandler.Unsubscribe(this);

    	CurrentInputType.MoveStarted -= OnMoveStarted;
    }
}
