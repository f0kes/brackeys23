using System;
using UnityEngine;

namespace Services.Projectile
{
	public interface IProjectile
	{
		public event Action<IProjectile> OnProjectileHit;
		public event Action<IProjectile> OnProjectileTick;
		
		Vector2 GetPosition();
		
		void Launch(ProjectileData data);
		void Destroy();
	}
}