using UnityEngine;

namespace Character
{
	public interface IMover
	{
		void Move(Vector2 direction);

		void LookAt(Vector2 spot);
	}
}