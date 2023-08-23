using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Services.Projectile
{
	public class Projectile : MonoBehaviour, IProjectile
	{
		public event Action<IProjectile> OnProjectileHit;
		public event Action<IProjectile> OnProjectileTick;

		protected float DistanceTravelled;

		protected ProjectileData Data;
		[FormerlySerializedAs("Rb")]
		[SerializeField] protected Rigidbody2D Rigidbody;

		protected float CurrentLifetime;


		public Vector2 GetPosition()
		{
			return transform.position;
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