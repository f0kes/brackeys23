using System;

namespace Characters
{
	public interface IDamagable
	{
		public event Action<int> OnHealthChanged;

		int GetHealth();

		void TakeDamage(int damage);

		void Die();

		int GetTeamId();
	}
}