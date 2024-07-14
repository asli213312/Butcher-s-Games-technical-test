public class ItemArchCollideGoodStrategy : ItemCollideStrategy
{
	protected override ICollidableItem ItemType { get => _goodItem; set => _goodItem = value as GoodArchItem; }
	protected override string ItemValuePattern => "+";
	private GoodArchItem _goodItem;

	protected override void Invoke() 
	{

	}
}