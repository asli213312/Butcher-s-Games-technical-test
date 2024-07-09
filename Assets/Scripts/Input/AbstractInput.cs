using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInput : IInput
{
	public event Action MoveStarted;
	public InputData InputData;
	protected Transform transform;
	protected Vector3 _lastMousePosition;
    protected bool _isDragging;

	public AbstractInput(InputData inputData, Transform transform) 
	{
		InputData = inputData;
		this.transform = transform;
	}

	public abstract void Handle();

	protected void StartMove() 
	{
		MoveStarted?.Invoke();
	}
}

public struct InputData
{
	public float SmoothSpeed;

	public InputData(float smoothSpeed) 
	{
		SmoothSpeed = smoothSpeed;
	}
}
