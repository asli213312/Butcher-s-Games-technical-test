using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform followObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    private Camera _camera;

    private bool _isActive;

    public void Initialize() 
    {
        _camera = Camera.main;
    }

    public void Activate() => _isActive = true;

    public void Handle() 
    {
        if (_isActive == false) return;

        Vector3 targetPosition = followObject.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        transform.position = smoothPosition;
    }
}
