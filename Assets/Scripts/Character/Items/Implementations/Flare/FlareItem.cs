using Characters.Movement;
using GameState;
using Services.Projectile;
using UnityEngine;

namespace Character.Items.Implementations.Flare
{
	public class FlareItem : MonoBehaviour, IItem, IProjectileHandler
	{
		[SerializeReference] private Projectile _flareProjectile;
		[SerializeField] private float _flareRange;
		[SerializeField] private float _flareSpeed;
		[SerializeField] private float _flareLifetime;

		public void Use(ICharacter user)
		{
			var gameManager = GameManager.Instance;
			var projectileService = gameManager.GetService<IProjectileService>();
			var direction = (user.GetLookSpot() - user.GetPosition()).normalized;
			var projectile = projectileService.CreateProjectile(_flareProjectile, new ProjectileData()
			{
				Range = _flareRange,
				StartPosition = user.GetPosition(),
				Velocity = direction * _flareSpeed,
				Lifetime = _flareLifetime,
			}, this);
		}

		public void OnProjectileHit(IProjectile projectile)
		{
		}

		public void OnProjectileTick(IProjectile projectile)
		{
		}
	}

}