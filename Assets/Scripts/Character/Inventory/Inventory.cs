using System.Collections.Generic;
using Character.Items;

namespace Character.Inventory
{
	public class Inventory : IInventory
	{
		private struct InventoryItem
		{
			public IItem Item;
			public int Count;
		}
		private readonly List<InventoryItem> _items = new List<InventoryItem>();
		public Inventory(int size, List<IItem> items = null)
		{
			for(var i = 0; i < size; i++)
			{
				_items.Add(new InventoryItem());
			}
			if(items == null) return;
			foreach(var item in items)
			{
				AddItem(item);
			}
		}
		private int FindItemIndex(IItem item)
		{
			int index = -1;
			for(var i = 0; i < _items.Count; i++)
			{
				if(_items[i].Item == null) continue;
				if(_items[i].Item.GetType() == item.GetType())
				{
					index = i;
					break;
				}
			}
			return index;
		}
		private int FindEmptyIndex()
		{
			return _items.FindIndex(i => i.Item == null);
		}
		public void AddItem(IItem item)
		{
			var index = FindItemIndex(item);
			if(index == -1)
			{
				index = FindEmptyIndex();
				if(index == -1) return;
				_items[index] = new InventoryItem { Item = item, Count = 1 };
			}
			else
			{
				_items[index] = new InventoryItem { Item = item, Count = _items[index].Count + 1 };
			}
		}

		public void RemoveItem(IItem item)
		{
			var index = FindItemIndex(item);
			if(index == -1) return;
			if(_items[index].Count == 1)
			{
				_items[index] = new InventoryItem();
			}
			else
			{
				_items[index] = new InventoryItem { Item = item, Count = _items[index].Count - 1 };
			}
		}

		public IItem GetItem(int index)
		{
			return _items[index].Item;
		}

		public IItem PullItem(int index)
		{
			var item = _items[index].Item;
			RemoveItem(item);
			return item;
		}

		public void SetItem(int index, IItem item)
		{
			_items[index] = new InventoryItem { Item = item, Count = 1 };
		}
	}
}