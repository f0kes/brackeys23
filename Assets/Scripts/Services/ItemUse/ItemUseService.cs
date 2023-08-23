using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Items;

namespace Services.ItemUse
{
	public class ItemUseService : IItemUseService
	{
		private List<Func<Character, IItem, bool>> _requirements = new();
		private List<Action<Character, IItem>> _effects = new();
		public void AddRequirement(Func<Character, IItem, bool> requirement)
		{
			_requirements.Add(requirement);
		}

		public void AddEffect(Action<Character, IItem> effect)
		{
			_effects.Add(effect);
		}

		public void UseItem(Character user, IItem item)
		{
			if(_requirements.Any(requirement => !requirement(user, item))) return;
			foreach(var effect in _effects)
			{
				effect(user, item);
			}
		}
	}
}