using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	private enum MoveStrategy { Linear }

	[SerializeField] private bool isTouchInput;
	[SerializeField] private float smoothSpeedSwipe;
	[SerializeField] private float moveSpeed;
	[SerializeField] private MoveStrategy moveStrategy;
	[SerializeField] private Transform followObject;

	public IInput CurrentInputType { get; private set; }
	private PlayerMoveStrategy _currentMoveStrategy;

	private bool _isMoving;

    public void Initialize()
    {
        CurrentInputType = isTouchInput ? new TouchInput(new InputData(smoothSpeedSwipe), followObject)
                                         : new MouseInput(new InputData(smoothSpeedSwipe), followObject);

		_currentMoveStrategy = SelectMoveStrategy();

		CurrentInputType.MoveStarted += OnMoveStarted;
    }

    public void Handle()
    {
        CurrentInputType.Handle();

        if (_isMoving)
        	_currentMoveStrategy.Handle();
    }

    private void OnMoveStarted() 
    {
    	_isMoving = true;
    }

    private PlayerMoveStrategy SelectMoveStrategy() 
    {
    	switch(moveStrategy) 
    	{
    		case MoveStrategy.Linear: return new PlayerMoveLinear(followObject, moveSpeed);
    		default: return null;
    	}
    }

    private void OnDestroy() 
    {
    	CurrentInputType.MoveStarted -= OnMoveStarted;
    }
}
