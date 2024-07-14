using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInput : IInput
{
	public event Action MoveStarted;
	public InputData InputData { get; set; }
	public bool IsActive { get; set; } = true;
	protected Transform transform;
	protected Vector3 _lastMousePosition;
	protected bool IsWorking;

	public AbstractInput(InputData inputData, Transform transform) 
	{
		InputData = inputData;
		this.transform = transform;
	}

	public void Handle() 
	{
		if (IsActive)
			OnHandle();
	}

	protected abstract void OnHandle();

	protected void StartMove() 
	{
		if (IsActive == false) return;

		MoveStarted?.Invoke();
	}
}

public struct InputData
{
	public Vector3 Direction;
	public float SmoothSpeed;
	public Vector2 Bounds;

	public InputData(float smoothSpeed, Vector2 bounds, Vector3 direction) 
	{
		SmoothSpeed = smoothSpeed;
		Bounds = bounds;
		Direction = direction;
	}
}
