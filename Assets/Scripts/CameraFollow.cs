using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform followObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private bool onStartActive;
    [SerializeField] private float smoothSpeed = 0.125f;

    private Camera _camera;

    private bool _isActive;

    private void Start() 
    {
        _isActive = onStartActive;
    }

    public void Initialize() 
    {
        _camera = Camera.main;
    }

    public void Activate() => _isActive = true;

    public void ChangeSpeed(float speed) => smoothSpeed = speed;

    public void ChangePositionOffset(Vector3 offset) 
    {
        _isActive = false;
        this.offset = offset;

        Vector3 targetPosition = followObject.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position = smoothPosition;

        this.WaitUntil(transform.position == smoothPosition, () => _isActive = true);
    }
    public void ChangeRotationOffset(Vector3 rotationOffset) 
    {
        this.rotationOffset = rotationOffset;
    }

    public void Handle() 
    {
        if (_isActive == false) return;

        Vector3 targetPosition = followObject.position + offset;
        Quaternion targetRotation = Quaternion.Euler(followObject.eulerAngles + rotationOffset);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        
        Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);

        //transform.position = smoothPosition;
        //transform.rotation = smoothRotation;
    }
}
