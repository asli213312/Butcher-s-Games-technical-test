using UnityEngine;

public abstract class ItemsData : ScriptableObject
{
    [SerializeField] public AudioClip soundOnPickup;
    [SerializeField] public int itemValue;
}