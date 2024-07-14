using UnityEngine;

public class ArchItemsController : MonoBehaviour
{
    [Zenject.Inject] private ItemController _itemController;

    [SerializeField] private AbstractArchItem[] archItems;

    public ICollidableItem[] Items => archItems;

    private void Start() 
    {
        Initialize();
    }

    private void Initialize() 
    {
        foreach (var archItem in archItems) 
        {
            if (archItem is not IItemCollectable) continue;

            if (archItem is AbstractItem abstractItem) 
            {
                switch (abstractItem as IItemCollectable) 
                {
                    case GoodArchItem: abstractItem.Initialize(_itemController.Config.goodArchItemsData.itemValue); break;
				    case BadArchItem: abstractItem.Initialize(_itemController.Config.badArchItemsData.itemValue); break;
                }
            }

        }
    }

    private void OnCollideItem(ICollidableItem item) 
    {
        Debug.Log("Arch items controller invoke collided");

        if (item is not AbstractArchItem archItem) return;

        OnCollideArchItem(archItem);
        Debug.Log("Collided arch item");
    }

    private void OnCollideArchItem(AbstractArchItem archItem) 
    {
        if (archItem is GoodArchItem goodItem)
            OnCollideGoodItem(goodItem);
        else if (archItem is BadArchItem badItem)
            OnCollideArchItem(badItem);
    }

    private void OnCollideGoodItem(GoodArchItem item) 
    {

    }

    private void OnCollideBadItem(BadArchItem item) 
    {

    }
}