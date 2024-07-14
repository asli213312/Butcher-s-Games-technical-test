using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class StateTrigger : MonoBehaviour
{
    public event Action Completed;
    public bool IsCompleted { get; private set; }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover)) 
        {
            IsCompleted = true;
            Completed?.Invoke();
        }
    }
}