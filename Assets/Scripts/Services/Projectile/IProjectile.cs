using System;

namespace Services.Projectile
{
	public interface IProjectile
	{
		public event Action<IProjectile> OnProjectileHit;
		public event Action<IProjectile> OnProjectileTick;
		
		void Launch(ProjectileData data);
		void Destroy();
	}
}