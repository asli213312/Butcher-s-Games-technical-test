using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickController : MonoBehaviour
{
    private readonly List<ITickable> _tickables = new();

    private void Update()
    {
        Notify();
    }

    public void Subscribe(ITickable observer) 
    {
        if (_tickables.Contains(observer) == false) 
        {
            _tickables.Add(observer);
        }
    }

    public void Unsubscribe(ITickable observer) 
    {
        if (_tickables.Contains(observer)) 
        {
            _tickables.Remove(observer);
        }
    }

    public void Notify() 
    {
        foreach (var item in _tickables)
        {
            item.Tick();
        }
    }
}