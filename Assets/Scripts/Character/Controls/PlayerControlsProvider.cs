using System;
using UnityEngine;

namespace Characters.Movement
{
	public class PlayerControlsProvider : MonoBehaviour, IControlsProvider
	{
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnLookAt;
		public event Action<int> OnChangeItem;
		public event Action OnUseItem;
		public event Action OnAttack;

		private void Update()
		{
			//var move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			var right = Input.GetKey(KeyCode.D) ? 1 : 0;
			var left = Input.GetKey(KeyCode.A) ? -1 : 0;
			var up = Input.GetKey(KeyCode.W) ? 1 : 0;
			var down = Input.GetKey(KeyCode.S) ? -1 : 0;
			var move = new Vector2(right + left, up + down);
			OnMove?.Invoke(move);

			var mousePosition = Input.mousePosition;
			if(Camera.main == null) return;
			var mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
			var lookAt = new Vector2(mousePositionInWorld.x, mousePositionInWorld.y);
			OnLookAt?.Invoke(lookAt);

			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				OnChangeItem?.Invoke(0);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				OnChangeItem?.Invoke(1);
			}
			if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				OnChangeItem?.Invoke(2);
			}
			//etc...

			if(Input.GetKeyDown(KeyCode.Space))
			{
				OnUseItem?.Invoke();
			}
		}

	}
}