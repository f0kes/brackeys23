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
		private Vector2 _lookDirection = Vector2.up;
		protected Vector2 FinalTarget;
		protected Vector2 NextWaypoint;

		protected IPathFindingService PathFindingService;
		protected ILightService LightService;
		private IControlsBinder _controlsBinder;
		private void Start()
		{
			var position = transform.position;
			FinalTarget = position;
			NextWaypoint = position;
			PathFindingService = GameManager.Instance.GetService<IPathFindingService>();
			LightService = GameManager.Instance.GetService<ILightService>();
			_controlsBinder = GameManager.Instance.GetService<IControlsBinder>();
			_controlsBinder.Bind(this, GetComponent<Character>()); //todo: do something with this
		}
		protected virtual Vector2 GetTarget()
		{
			var lightSources = LightService.GetLightSources();
			//var temp = lightSources.OfType<IDamagable>().Cast<ILightSource>().ToList();
			//lightSources = temp;

			//we will sort both by intensity and distance, and choose that with the highest sum rank
			float maxRank = float.MinValue;
			var maxRankTarget = Vector2.zero;
			foreach(var lightSource in lightSources)
			{
				var lightSourcePosition = lightSource.GetPosition();
				var distance = Vector2.Distance(transform.position, lightSourcePosition);
				var intensity = lightSource.GetIntensity();
				var rank = intensity / distance;
				if(!(rank > maxRank)) continue;
				maxRank = rank;
				maxRankTarget = lightSourcePosition;
			}
			return maxRankTarget;
		}
		protected virtual void Update()
		{
			FinalTarget = GetTarget();


			if(FinalTarget == Vector2.zero)
			{
				return;
			}

			if(Vector2.Distance(transform.position, NextWaypoint) < 100f)
			{
				NextWaypoint = PathFindingService.GetNextPosition(transform.position, FinalTarget);
			}

			_lookDirection = Vector3.RotateTowards(_lookDirection, NextWaypoint - (Vector2)transform.position, _rotationSpeed * Time.deltaTime, 0f);
			OnLookAt?.Invoke(_lookDirection);
			OnMove?.Invoke(_lookDirection);
			OnAttack?.Invoke();
		}
	}
}