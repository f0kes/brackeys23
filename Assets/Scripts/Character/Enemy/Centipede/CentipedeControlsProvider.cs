using System;
using Characters.Movement;
using GameState;
using Services.Light;
using Services.Pathfinding;
using UnityEngine;

namespace Characters.Enemy.Centipede
{
	[RequireComponent(typeof(Character))]
	public class CentipedeControlsProvider : MonoBehaviour, IControlsProvider //TODO: Remove code duplication with EnemyControlsProvider
	{
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnLookAt;
		public event Action<int> OnChangeItem;
		public event Action OnUseItem;
		public event Action OnAttack;
		public event Action OnStartRunning;
		public event Action OnStopRunning;

		[SerializeField] private float _rotationSpeed;
		[SerializeField] private float _aggroRange;
		private Vector2 _lookDirection = Vector2.up;
		protected Vector2 FinalTarget;
		protected Vector2 NextWaypoint;

		protected IPathFindingService PathFindingService;
		protected ILightService LightService;
		private IControlsBinder _controlsBinder;
		private Func<Vector2> _targetFunc;

		private float _abilityAggroRange;
		private float _playerAggroRange;
		private float _aggroTime;
		private Vector2 _defaultPosition;
		private float _minAggroLightIntensity;

		private bool _aggro;
		private float _aggroTimer;
		private void Awake()
		{
			_targetFunc = GetClosestLight;
		}
		private void Start()
		{
			var position = transform.position;
			FinalTarget = position;
			NextWaypoint = position;
			PathFindingService = GameManager.Instance.GetService<IPathFindingService>();
			LightService = GameManager.Instance.GetService<ILightService>();
			_controlsBinder = GameManager.Instance.GetService<IControlsBinder>();
			_controlsBinder.Bind(this, GetComponent<Character>()); //todo: do something with this
			LightService.OnLightEvent += OnLight;
		}

		private void OnLight(Vector2 obj)
		{
			if(Vector2.Distance(transform.position, obj) < _abilityAggroRange)
			{
				Aggro();
			}
		}
		private bool IsPlayerInAggroRange()
		{
			var isInRange = Vector2.Distance(transform.position, Player.Player.Instance.GetPosition()) < _playerAggroRange;
			return Vector2.Distance(transform.position, Player.Player.Instance.GetPosition()) < _playerAggroRange;
		}
		private void Aggro()
		{
			if(_aggro) return;
			_targetFunc = () => Player.Player.Instance.GetPosition();
			_aggro = true;
			_aggroTimer = _aggroTime;
		}

		public void SetMinAggroLightIntensity(float minAggroLightIntensity)
		{
			_minAggroLightIntensity = minAggroLightIntensity;
		}
		public void SetAggroTime(float aggroTime)
		{
			_aggroTime = aggroTime;
		}
		public void SetAggroRange(float aggroRange)
		{
			_aggroRange = aggroRange;
		}
		public void SetDefaultPosition(Vector2 defaultPosition)
		{
			_defaultPosition = defaultPosition;
		}
		private Vector2 GetClosestLight()
		{
			var lightSources = LightService.GetLightSources();
			var maxRank = float.MinValue;
			var maxRankTarget = Vector2.zero;
			foreach(var lightSource in lightSources)
			{
				var lightSourcePosition = lightSource.GetPosition();
				var distance = Vector2.Distance(transform.position, lightSourcePosition);
				if(distance > _aggroRange) continue;
				var intensity = lightSource.GetIntensity();
				if(intensity < _minAggroLightIntensity) continue;
				var rank = intensity / distance;
				if(!(rank > maxRank)) continue;
				maxRank = rank;
				maxRankTarget = lightSourcePosition;
			}
			return maxRankTarget;
		}
		protected virtual Vector2 GetTarget()
		{
			return _targetFunc();
		}
		protected virtual void Update()
		{
			if(_aggro) _aggroTimer -= Time.deltaTime;
			if(_aggroTimer <= 0)
			{
				_aggro = false;
				_targetFunc = () => _defaultPosition;
			}
			if(IsPlayerInAggroRange()) Aggro();

			FinalTarget = GetClosestLight();


			if(FinalTarget == Vector2.zero)
			{
				FinalTarget = _defaultPosition;
			}

			if(Vector2.Distance(transform.position, NextWaypoint) < 100f)
			{
				NextWaypoint = PathFindingService.GetNextPosition(transform.position, FinalTarget);
			}

			var position = transform.position;
			_lookDirection = Vector3.RotateTowards(_lookDirection, NextWaypoint - (Vector2)position, _rotationSpeed * Time.deltaTime, 0f);
			OnLookAt?.Invoke(((Vector2)position + _lookDirection));
			OnMove?.Invoke(_lookDirection);
			OnAttack?.Invoke();
		}
	}
}