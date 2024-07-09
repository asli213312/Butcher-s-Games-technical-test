using System;
using UnityEngine;

public interface IInput
{
	event Action MoveStarted;
	void Handle();
}
