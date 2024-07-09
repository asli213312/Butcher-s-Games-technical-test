using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : AbstractInput
{
    public TouchInput(InputData inputData, Transform transform) : base(inputData, transform)
    {
        
    }

    public override void Handle() 
    {
    	if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _lastMousePosition = touch.position;
                _isDragging = true;
                StartMove();
            }

            if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && _isDragging)
            {
                Vector3 currentTouchPosition = touch.position;
                Vector3 delta = currentTouchPosition - _lastMousePosition;
                Vector3 targetPosition = transform.position + new Vector3(delta.x * Time.deltaTime, 0, 0);

                transform.position = Vector3.Lerp(transform.position, targetPosition, InputData.SmoothSpeed);

                _lastMousePosition = currentTouchPosition;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _isDragging = false;
            }
        }
    }
}
