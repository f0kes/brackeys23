using System;
using System.Collections.Generic;
using Characters.Movement;
using Misc;
using Services.Light;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Items.Implementations.JellyFish
{
	public class JellyFishItem : Item
	{
		[SerializeField] private LightSource _lampPrefab;
		[SerializeField] private float _innerSpawnRadius;
		[SerializeField] private float _outerSpawnRadius;
		[SerializeField] private int _lampCount;
		[FormerlySerializedAs("_lampAcceleration")]
		[SerializeField] private float _minLampAcceleration;
		[SerializeField] private float _maxLampAcceleration;
		[SerializeField] private float _lifetime;
		[SerializeField] private float _activationTime;
		[SerializeField] private float _deactivationTime;
		[SerializeField] private Color _lampColor;
		[SerializeField] private float _lampIntensity;

		private Dictionary<LightSource, LampState> _lampStates;
		private readonly List<LightSource> _lampPool = new List<LightSource>();
		private struct LampState
		{
			public enum LampStateType
			{
				Active,
				Deactivating,
				Inactive,
				Activating
			}

			public Vector2 TargetOffset;
			public Vector2 CurrentOffset;
			public Vector2 AnchorPosition;
			public float CurrentLifetime;
			public ICharacter Target;
			public LampStateType State;
			public Vector2 Velocity;
			public float Acceleration;

		}

		private void Awake()
		{
			_lampStates = new Dictionary<LightSource, LampState>();
			for(int i = 0; i < _lampCount; i++)
			{
				var lamp = Instantiate(_lampPrefab, transform);
				InitializeLamp(lamp);
				lamp.gameObject.SetActive(false);
				_lampStates.Add(lamp, new LampState { State = LampState.LampStateType.Inactive });
				_lampPool.Add(lamp);
			}
		}



		public override void Use(ICharacter user)
		{
			base.Use(user);
			StartActivatingLamps(user);
			RandomizeOffsets();
		}

		public override void Update()
		{
			base.Update();
			foreach(var lamp in _lampPool)
			{
				HandleLamp(lamp);
			}
		}
		private void HandleLamp(LightSource lamp)
		{
			var state = _lampStates[lamp];
			switch(state.State)
			{
				case LampState.LampStateType.Active:
					HandleActive(lamp);
					break;
				case LampState.LampStateType.Deactivating:
					HandleDeactivating(lamp);
					break;
				case LampState.LampStateType.Inactive:
					HandleInactive(lamp);
					break;
				case LampState.LampStateType.Activating:
					HandleActivating(lamp);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		private void RandomizeOffsets()
		{
			foreach(var lamp in _lampPool)
			{
				var state = _lampStates[lamp];
				var direction = UnityEngine.Random.insideUnitCircle.normalized;
				var offset = direction * UnityEngine.Random.Range(_innerSpawnRadius, _outerSpawnRadius);
				state.TargetOffset = offset;
				_lampStates[lamp] = state;
			}
		}
		private void StartActivatingLamps(ICharacter user)
		{
			foreach(var lamp in _lampPool)
			{
				var state = new LampState
				{
					State = LampState.LampStateType.Activating,
					Target = user,
					CurrentLifetime = 0,
					AnchorPosition = user.GetPosition(),
					Acceleration = UnityEngine.Random.Range(_minLampAcceleration, _maxLampAcceleration)
				};
				lamp.gameObject.SetActive(true);
				lamp.SetPosition(user.GetPosition());
				InitializeLamp(lamp);
				_lampStates[lamp] = state;
			}
		}

		private void MoveToTarget(LightSource lamp, ref LampState final)
		{
			var state = final;
			var targetPosition = state.Target.GetPosition();
			var anchorPos = state.AnchorPosition;
			var direction = (targetPosition - anchorPos).normalized;

			state.Velocity += direction * state.Acceleration * Time.deltaTime;
			state.AnchorPosition += state.Velocity * Time.deltaTime;


			lamp.SetPosition(state.AnchorPosition + state.CurrentOffset);

			_lampStates[lamp] = state;
			final = state;
		}

		#region Handlers

		private void HandleActive(LightSource lamp)
		{
			var state = _lampStates[lamp];
			state.CurrentLifetime += Time.deltaTime;
			if(state.Target != null)
			{
				MoveToTarget(lamp, ref state);
			}
			if(state.CurrentLifetime >= _lifetime)
			{
				state.State = LampState.LampStateType.Deactivating;
				state.CurrentLifetime = 0;
			}
			_lampStates[lamp] = state;
		}
		private void HandleDeactivating(LightSource lamp)
		{
			var state = _lampStates[lamp];
			state.CurrentLifetime += Time.deltaTime;
			if(state.Target != null)
			{
				MoveToTarget(lamp, ref state);
			}
			var blend = Tween.EaseInSine(state.CurrentLifetime / _deactivationTime);
			state.CurrentOffset = Vector2.Lerp(state.TargetOffset, Vector2.zero, blend);
			lamp.SetIntensity(1 - blend);
			if(state.CurrentLifetime >= _deactivationTime)
			{
				lamp.gameObject.SetActive(false);
				state.State = LampState.LampStateType.Inactive;
				state.CurrentLifetime = 0;
			}
			_lampStates[lamp] = state;
		}
		private void HandleInactive(LightSource lamp)
		{
		}
		private void HandleActivating(LightSource lamp)
		{
			var state = _lampStates[lamp];
			state.CurrentLifetime += Time.deltaTime;
			if(state.Target != null)
			{
				MoveToTarget(lamp, ref state);
			}
			var blend = Tween.EaseOutSine(state.CurrentLifetime / _activationTime);
			state.CurrentOffset = Vector2.Lerp(Vector2.zero, state.TargetOffset, blend);
			if(state.CurrentLifetime >= _activationTime)
			{
				state.State = LampState.LampStateType.Active;
				state.CurrentLifetime = 0;
			}
			_lampStates[lamp] = state;
		}

		#endregion


		private void InitializeLamp(ILightSource lamp)
		{
			lamp.SetColor(_lampColor);
			lamp.SetIntensity(_lampIntensity);
		}

		public override Vector2 GetAnchor(ICharacter user)
		{
			return user.GetPosition();
		}
	}
}