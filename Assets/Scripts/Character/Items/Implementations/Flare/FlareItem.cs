using Characters.Movement;
using GameState;
using Services.Projectile;
using UnityEngine;

namespace Characters.Items.Implementations.Flare
{
	public class FlareItem : Item, IProjectileHandler
	{
		[SerializeReference] private Projectile _flareProjectile;
		[SerializeField] private float _flareRange;
		[SerializeField] private float _flareSpeed;
		[SerializeField] private float _flareLifetime;
		private Vector2 _flarePosition;
		public override void Use(ICharacter user)
		{
			base.Use(user);
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

		public override Vector2 GetAnchor(ICharacter user)
		{
			return _flarePosition;
		}

		public void OnProjectileHit(IProjectile projectile)
		{
		}

		public void OnProjectileTick(IProjectile projectile)
		{
			_flarePosition = projectile.GetPosition();
		}
	}

}