using System;
using Characters;
using GameState;
using Services.Light;
using UnityEngine;

namespace Light
{
	public class DamagableLightSource : LightSource, IDamagable
	{
		[SerializeField] private int _health;

		public event Action<int> OnHealthChanged;

		private float _initialIntensity;
		private int _initialHealth;
		public int GetHealth()
		{
			return _health;
		}

		protected override void Start()
		{
			base.Start();
			_initialIntensity = GetIntensity();
			_initialHealth = _health;
		}

		public void TakeDamage(int damage)
		{
			_health -= damage;
			OnHealthChanged?.Invoke(_health);
			var intensity = Mathf.Lerp(0, _initialIntensity, (float)_health / _initialHealth);
			SetIntensity(intensity);
			
			if(_health <= 0)
			{
				Die();
			}
		}

		public void Die()
		{
			Destroy(gameObject);
		}

		public int GetTeamId()
		{
			return 0;
		}
	}
}