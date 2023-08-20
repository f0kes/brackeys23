using System;

namespace Services.Projectile
{
	public interface IProjectileHandler
	{
		void OnProjectileHit(IProjectile projectile);

		void OnProjectileTick(IProjectile projectile);
	}
}