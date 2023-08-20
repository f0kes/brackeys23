namespace Services.Projectile
{
	public interface IProjectileService
	{
		IProjectile CreateProjectile(Projectile projectile, ProjectileData data, IProjectileHandler handler);

		void DestroyProjectile(Projectile projectile);
	}
}