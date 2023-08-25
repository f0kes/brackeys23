using UnityEngine;

namespace Services.Projectile
{
	public struct ProjectileData
	{
		public Characters.Character Owner{get; set;}
		public Vector2 Velocity{get; set;}
		public Vector2 StartPosition{get; set;}
		public float Range{get; set;}
		public float Lifetime{get; set;}
		public int Damage { get; set; }

	}
}