using System;
using GameState;
using UnityEngine;

namespace Character
{
	public class PlayerControlsProvider : MonoBehaviour, IControlsProvider
	{
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnLookAt;

		private void Update()
		{
			var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			OnMove?.Invoke(move);

			var mousePosition = Input.mousePosition;
			if(Camera.main == null) return;
			var mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
			var lookAt = new Vector2(mousePositionInWorld.x, mousePositionInWorld.y);
			OnLookAt?.Invoke(lookAt);
		}

	}
}