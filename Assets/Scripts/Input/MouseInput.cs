using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : AbstractInput
{
    public MouseInput(InputData inputData, Transform transform) : base(inputData, transform)
    {
        
    }

    public override void Handle() 
    {
    	if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
            _isDragging = true;
            StartMove();
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 delta = currentMousePosition - _lastMousePosition;
            Vector3 targetPosition = transform.position + new Vector3(delta.x * Time.deltaTime, 0, 0);

            transform.position = Vector3.Lerp(transform.position, targetPosition, InputData.SmoothSpeed);

            _lastMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }
}
