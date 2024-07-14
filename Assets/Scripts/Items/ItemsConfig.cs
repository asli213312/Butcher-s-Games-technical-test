using UnityEngine;

[CreateAssetMenu(menuName = "Game/Items/Config")]
public class ItemsConfig : ScriptableObject
{
    [SerializeField, SerializeReference] public GoodItemsData goodItemsData;
    [SerializeField, SerializeReference] public BadItemsData badItemsData;
    [SerializeField] public GoodArchItemsData goodArchItemsData;
    [SerializeField] public BadArchItemsData badArchItemsData;
}