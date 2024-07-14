public class ItemArchCollideBadStrategy : ItemCollideStrategy
{
	protected override ICollidableItem ItemType { get => _goodItem; set => _goodItem = value as BadArchItem; }
	protected override string ItemValuePattern => "";
	private BadArchItem _goodItem;

	protected override void Invoke() 
	{

	}
}