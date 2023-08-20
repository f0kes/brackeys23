using UnityEngine;

namespace Services.Projectile
{
	public class ProjectileService : IProjectileService
	{

		public IProjectile CreateProjectile(Projectile projectile, ProjectileData data, IProjectileHandler handler)
		{
			var projectileInstance = Object.Instantiate(projectile);
			projectileInstance.Launch(data);
			projectileInstance.OnProjectileHit += handler.OnProjectileHit;
			projectileInstance.OnProjectileTick += handler.OnProjectileTick;

			projectileInstance.transform.position = data.StartPosition;
			//projectileInstance.transform.rotation = Quaternion.LookRotation(data.Velocity);
			projectileInstance.gameObject.SetActive(true);
			return projectileInstance;
		}

		public void DestroyProjectile(Projectile projectile)
		{
			Object.Destroy(projectile.gameObject);
		}
	}
}