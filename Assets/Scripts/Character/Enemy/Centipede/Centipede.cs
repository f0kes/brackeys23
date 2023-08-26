using System;
using GameState;
using Services.Light;
using UnityEngine;

namespace Characters.Enemy.Centipede
{
	public class Centipede : Enemy
	{
		[SerializeField] private CentipedeControlsProvider _controlsProvider;

		[SerializeField] private float _aggroRange;

		[SerializeField] private int _damage;
		[SerializeField] private float _attackTime;
		[SerializeField] private float _aggroTime;
		[SerializeField] private float _slowTime;
		[SerializeField] private float _slowPercent;
		[SerializeField] private float _minLightAggroIntensity = 0.2f;

		private Vector2 _defaultPosition;


		protected override void Start()
		{
			base.Start();
			_defaultPosition = transform.position;

			_controlsProvider.SetAggroRange(_aggroRange);
			_controlsProvider.SetAggroTime(_aggroTime);
			_controlsProvider.SetDefaultPosition(_defaultPosition);
			_controlsProvider.SetMinAggroLightIntensity(_minLightAggroIntensity);
		}

		public override int GetDamage()
		{
			return _damage;
		}

		public override float GetAttackSpeed()
		{
			return _attackTime;
		}

		protected override void ProcessDamage(IDamagable damagable)
		{
			base.ProcessDamage(damagable);
			if(damagable is Player.Player player)
			{
				player.SetSlow(_slowPercent, _slowTime);
			}
		}
		

	}
}