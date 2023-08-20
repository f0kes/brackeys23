using System;
using UnityEngine;

namespace Character
{
	public interface IControlsProvider
	{
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnLookAt;
	}
}