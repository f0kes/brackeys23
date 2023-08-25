using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Enemy
{
	public class ParanoidPupil : MonoBehaviour
	{
		[SerializeField] private float shiftDistance;
		[SerializeField] private float _zOffset;

		public Vector2 startPosition;



		private void Start()
		{
			startPosition = transform.position;
		}

		private void Update()
		{
			var target = Characters.Player.Player.Instance.GetPosition();
			Vector3 position = startPosition + (target - startPosition).normalized * shiftDistance;
			position = new Vector3(position.x, position.y, _zOffset);
			transform.position = position;
		}
	}
}