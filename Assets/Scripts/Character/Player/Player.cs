using System;
using Services.Light;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters.Player
{
	public class Player : Character, IPlayer
	{
		public event Action OnStartRunning;
		public event Action OnStopRunning;
		public static Player Instance{get; private set;}
		[SerializeField] private float _stepLength;
		[SerializeField] private PlayerLight _lightSource;
		[SerializeField] private PlayerData _playerData;
		private Vector2 _lastStepPosition;

		private bool _canRun = true;
		private bool _isRunning;
		private float _runTimer;
		private float _runCooldownTimer;

		protected override void Awake()
		{
			if(Instance != null)
				throw new Exception("Multiple instances of Player");
			Instance = this;
			SetPlayerData(Instantiate(_playerData));
			base.Awake();
		}
		protected override void Start()
		{
			base.Start();
			_lastStepPosition = transform.position;
		}
		private void Update()
		{
			HandleRunning();
			if(!((_lastStepPosition - (Vector2)transform.position).magnitude >= _stepLength)) return;
			_lastStepPosition = transform.position;
			_lightSource.Blink();
		}
		private void HandleRunning()
		{
			if(_isRunning)
			{
				_runTimer += Time.deltaTime;
				if(_runTimer >= AccelerationTime)
				{
					_runTimer = 0;
					StopRunning();
				}
			}
			else
			{
				_runCooldownTimer += Time.deltaTime;
				if(_runCooldownTimer >= AccelerationCooldown)
				{
					_canRun = true;
				}
			}
		}

		public void SetPlayerData(PlayerData data)
		{
			_maxHealth = data.MaxHealth;
			_speed = data.Velocity;
			_runningSpeed = data.AcceleratedVelocity;
			_currentSpeed = _speed;
			AccelerationTime = data.AccelerationTime;
			AccelerationCooldown = data.AccelerationCooldown;
		}

		public override void StartRunning()
		{
			if(!_canRun) return;
			_canRun = false;
			base.StartRunning();
			_isRunning = true;
			OnStartRunning?.Invoke();
		}

		public override void StopRunning()
		{
			if(!_isRunning) return;
			_runCooldownTimer = 0;
			base.StopRunning();
			_isRunning = false;
			OnStopRunning?.Invoke();
		}

		public ILightSource GetBlinkSource()
		{
			return _lightSource;
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