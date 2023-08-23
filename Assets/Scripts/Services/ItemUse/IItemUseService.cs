using System;
using Characters;
using Characters.Items;

namespace Services.ItemUse
{
	public interface IItemUseService
	{
		void AddRequirement(Func<Character, IItem, bool> requirement);

		void AddEffect(Action<Character, IItem> effect);

		void UseItem(Character user, IItem item);
	}
}