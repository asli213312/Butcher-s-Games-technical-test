using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private IInput _input;

	public InputHandler(IInput inputType) 
    {
        _input = inputType;
    }

    public void Handle() 
    {
        _input.Handle();
    }
}
