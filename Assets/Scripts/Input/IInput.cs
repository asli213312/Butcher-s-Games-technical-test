using System;
using UnityEngine;

public interface IInput
{
	event Action MoveStarted;
	InputData InputData { get; set; }
	bool IsActive { get; set; }
	void Handle();
}
