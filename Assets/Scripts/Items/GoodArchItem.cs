using UnityEngine;

public class GoodArchItem : AbstractArchItem, IGoodItem
{
    [SerializeField] private bool isMoney;

    public bool IsMoney => isMoney;

    protected override void OnCollide() 
    {

    }
}