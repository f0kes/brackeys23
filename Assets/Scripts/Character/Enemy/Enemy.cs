using UnityEngine;

namespace Characters.Enemy
{
	public class Enemy : Character
	{
		[SerializeField] private float _lifeTime;

		protected override void Start()
		{
			base.Start();
			Destroy(gameObject, _lifeTime);
		}
	}
}