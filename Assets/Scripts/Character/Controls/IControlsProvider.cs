using System;
using UnityEngine;

namespace Characters.Movement
{
	public interface IControlsProvider
	{
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnLookAt;
		public event Action<int> OnChangeItem;
		public event Action OnUseItem;
		public event Action OnAttack;
	}
}
