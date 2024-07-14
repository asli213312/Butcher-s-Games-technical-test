using UnityEngine;

public interface IObservable<T> where T : MonoBehaviour 
{
    void Subscribe(IObserver<T> observer);
    void Unsubscribe(IObserver<T> observer);
    void Notify();
}

public interface IObserver<T> where T : MonoBehaviour 
{
    void OnNotify<T>();
}