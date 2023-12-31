﻿using System;
using System.Collections;
using Services.Light;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
		[SerializeField] private AudioClip[] stepAudioClips;
		[SerializeField] private float walkStepDelay;
		[SerializeField] private float runStepDelay;

		[SerializeField] private PlayerAnimationBehaviour animate;
		private Vector2 _lastStepPosition;

		private bool _canRun = true;
		private bool _isRunning;
		private float _runTimer;
		private float _runCooldownTimer;
		private float _slow;
		private float _slowTimer;
		private bool _slowed = false;

		protected override void Awake()
		{
			if(Instance != null)
				throw new Exception("Multiple instances of Player");
			Instance = this;
			SetPlayerData(Instantiate(_playerData));
			base.Awake();
			StartCoroutine(HandleStepSounds());
			animate.playIdleAnimation();
		}
		protected override void Start()
		{
			base.Start();
			_lastStepPosition = transform.position;
		}
		public override void Update()
		{
			HandleRunning();
			_slowTimer -= Time.deltaTime;
			if(_slowTimer <= 0 && _slowed)
			{
				Debug.Log("Remove slow");
				RemoveSlow();
			}
			if(((_lastStepPosition - (Vector2)transform.position).magnitude >= _stepLength))
			{
				_lastStepPosition = transform.position;
				_lightSource.Blink();
			}
		}

		public void SetSlow(float percentage, float time)
		{
			_slow = percentage;

			Debug.Log("Set slow, old speed: " + _speed);
			_speed = _playerData.Velocity * (1 - _slow);
			_runningSpeed = _playerData.AcceleratedVelocity * (1 - _slow);
			Debug.Log("Set slow, new speed: " + _speed);


			_slowTimer = time;
			_slowed = true;
		}
		public void RemoveSlow()
		{
			Debug.Log("Remove slow, old speed: " + _speed);
			_speed = _playerData.Velocity;
			_runningSpeed = _playerData.AcceleratedVelocity;
			Debug.Log("Remove slow, new speed: " + _speed);
			_slow = 0;
			_slowed = false;
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

		public override void Move(Vector2 direction)
		{
			base.Move(direction);
			animate.SendLegsDirection(direction);
			if(IsMoving)
			{
				if(_isRunning)
				{
					animate.playRunAnimation();
				}
				else
				{
					animate.playWalkAnimation();
				}
			}
			else
			{
				animate.playIdleAnimation();
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

		private IEnumerator HandleStepSounds()
		{
			while (true)
			{
				if(!IsMoving)
				{
					yield return new WaitForSeconds(0.1f);
					continue;
				}
				GetAudioSource().PlayOneShot(stepAudioClips[Random.Range(0, stepAudioClips.Length)]);
				yield return new WaitForSeconds(_isRunning ? runStepDelay : walkStepDelay);
			}
		}
	}
}