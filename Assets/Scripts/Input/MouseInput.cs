using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : AbstractInput
{
    private Rigidbody _rb;
    private Vector3 _initialPosition;

    public MouseInput(InputData inputData, Transform transform) : base(inputData, transform)
    {
        _rb = transform.GetComponent<Rigidbody>();
        _initialPosition = transform.position;
    }

    protected override void OnHandle() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
            IsWorking = true;
            StartMove();
        }

        if (Input.GetMouseButton(0) && IsWorking)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 delta = currentMousePosition - _lastMousePosition;

            Vector3 localSwipeDirection = transform.TransformDirection(new Vector3(delta.x, 0, delta.y)).normalized;

            // Умножаем направление свайпа на скорость из InputData.SmoothSpeed
            float swipeSpeed = InputData.SmoothSpeed;
            Vector3 moveAmount = localSwipeDirection * swipeSpeed * Time.deltaTime;

            Vector3 targetPosition = transform.position + moveAmount;
            transform.position = targetPosition;

            _lastMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsWorking = false;
        }
    }

    /*protected override void OnHandle() 
    {
    	if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
            IsWorking = true;
            StartMove();
        }

        if (Input.GetMouseButton(0) && IsWorking)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 delta = currentMousePosition - _lastMousePosition;

            Vector3 targetPosition = transform.position;

            float targetDir = 0;

            if (InputData.Direction.x > 0) 
            {
                targetDir = transform.position.x + delta.x * Time.deltaTime;
                targetDir = Mathf.Clamp(targetDir, InputData.Bounds.x, InputData.Bounds.y);

                targetPosition = new Vector3(targetDir, transform.position.y, transform.position.z);  
            }
            else if (InputData.Direction.y > 0) 
            {
                targetDir = transform.position.y + delta.y * Time.deltaTime;
                targetDir = Mathf.Clamp(targetDir, InputData.Bounds.x, InputData.Bounds.y);

                targetPosition = new Vector3(transform.position.x, targetDir, transform.position.z);  
            }
            else
            {
                targetDir = transform.position.z + delta.x * Time.deltaTime;
                targetDir = Mathf.Clamp(targetDir, InputData.Bounds.x + transform.position.z, InputData.Bounds.y + transform.position.z);

                targetPosition = new Vector3(transform.position.x, transform.position.y, targetDir);
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, InputData.SmoothSpeed);

            _lastMousePosition = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsWorking = false;
        }
    }*/
}
