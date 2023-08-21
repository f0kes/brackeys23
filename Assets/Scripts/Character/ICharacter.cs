using UnityEngine;

namespace Characters.Movement
{
	public interface ICharacter
	{
		void Move(Vector2 direction);

		Vector2 GetPosition();

		void LookAt(Vector2 spot);

		Vector2 GetLookSpot();

		void ChangeItem(int itemIndex);

		void UseItem();

		void Attack();
	}
}