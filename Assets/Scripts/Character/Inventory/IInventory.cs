using Character.Items;

namespace Character.Inventory
{
	public interface IInventory
	{
		void AddItem(IItem item);

		void RemoveItem(IItem item);

		IItem GetItem(int index);

		IItem PullItem(int index);

		void SetItem(int index, IItem item);
		
	}
}