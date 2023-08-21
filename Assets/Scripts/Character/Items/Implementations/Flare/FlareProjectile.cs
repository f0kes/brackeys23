using System;
using Services.Projectile;
using UnityEngine;

namespace Characters.Items.Implementations.Flare
{
	public class FlareProjectile : Projectile
	{
		private float _currentVelocity;
		

		public override void Launch(ProjectileData data)
		{
			base.Launch(data);
			_currentVelocity = data.Velocity.magnitude;
		}

		protected override void Move()
		{
			_currentVelocity = Mathf.Lerp(_currentVelocity, 0, Time.fixedDeltaTime);
			Rigidbody.velocity = Data.Velocity.normalized * _currentVelocity;
			DistanceTravelled += _currentVelocity * Time.fixedDeltaTime;
			if(DistanceTravelled >= Data.Range)
			{
				Destroy(gameObject);
			}
		}
	}
}