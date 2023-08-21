using System;
using Services.Light;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters.Player
{
	public class Player : Character
	{
		public static Player Instance{get; private set;}
		[SerializeField] private float _stepLength;
		[SerializeField] private PlayerLight _lightSource;
		private Vector2 _lastStepPosition;

		protected override void Awake()
		{
			if(Instance != null)
				throw new Exception("Multiple instances of Player");
			Instance = this;
			base.Awake();
		}
		protected override void Start()
		{
			base.Start();
			_lastStepPosition = transform.position;
		}

		private void Update()
		{
			if(!((_lastStepPosition - (Vector2)transform.position).magnitude >= _stepLength)) return;
			_lastStepPosition = transform.position;
			_lightSource.Blink();
		}

		public override void Die()
		{
			//restart scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		public override int GetTeamId()
		{
			return 0;
		}
	}
}