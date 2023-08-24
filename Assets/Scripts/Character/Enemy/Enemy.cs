using UnityEngine;

namespace Characters.Enemy
{
	public class Enemy : Character
	{
		[SerializeField] private float _lifeTime;

		protected override void Start()
		{
			base.Start();
			if(_lifeTime > 0) Destroy(gameObject, _lifeTime);
		}
	}
}