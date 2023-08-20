using System;
using UnityEngine;

namespace GameState
{
	public class Ticker : MonoBehaviour, ITicker
	{
		[SerializeField] private float _tickInterval;
		public event Action OnTick;

		public float TickInterval => _tickInterval;
		private float _timeSinceLastTick;

		private void Awake()
		{
			GameManager.Instance.RegisterService<ITicker>(this, true);
		}
		private void Update()
		{
			_timeSinceLastTick += Time.deltaTime;
			if(!(_timeSinceLastTick >= _tickInterval)) return;
			OnTick?.Invoke();
			_timeSinceLastTick = 0;
		}

	}
}