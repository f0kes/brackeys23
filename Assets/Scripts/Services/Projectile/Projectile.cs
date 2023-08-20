using System;
using UnityEngine;

namespace Services.Projectile
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class Projectile : MonoBehaviour, IProjectile
	{
		public event Action<IProjectile> OnProjectileHit;
		public event Action<IProjectile> OnProjectileTick;

		protected float DistanceTravelled;

		protected ProjectileData Data;
		protected Rigidbody2D Rigidbody;

		protected float CurrentLifetime;

		private void Awake()
		{
			Rigidbody = GetComponent<Rigidbody2D>();
		}
		public virtual void Launch(ProjectileData data)
		{
			Data = data;
		}

		public virtual void Destroy()
		{
		}
		private void FixedUpdate()
		{
			OnProjectileTick?.Invoke(this);
			Move();
			CurrentLifetime += Time.fixedDeltaTime;
			if(CurrentLifetime >= Data.Lifetime)
			{
				Destroy(gameObject);
			}
		}
		protected virtual void Move()
		{
			Rigidbody.velocity = Data.Velocity;
			DistanceTravelled += Data.Velocity.magnitude * Time.fixedDeltaTime;
			if(DistanceTravelled >= Data.Range)
			{
				Destroy(gameObject);
			}
		}
		private void OnDestroy()
		{
			OnProjectileHit?.Invoke(this);
		}
	}
}