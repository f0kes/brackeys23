using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Movement;
using GameState;
using Services.Light;
using Services.Pathfinding;
using UnityEngine;

namespace Characters.Enemy
{
	public class EnemyControlsProvider : MonoBehaviour, IControlsProvider
	{

		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnLookAt;
		public event Action<int> OnChangeItem;
		public event Action OnUseItem;
		public event Action OnAttack;
		public event Action OnStartRunning;
		public event Action OnStopRunning;

		[SerializeField] private float _aggroRange;
		[SerializeField] private float _minAggroLightIntensity = 0.2f;

		protected Vector2 FinalTarget;
		protected Vector2 NextWaypoint;

		protected IPathFindingService PathFindingService;
		protected ILightService LightService;
		private void Start()
		{
			var position = transform.position;
			FinalTarget = position;
			NextWaypoint = position;
			PathFindingService = GameManager.Instance.GetService<IPathFindingService>();
			LightService = GameManager.Instance.GetService<ILightService>();
		}
		protected virtual Vector2 GetTarget()
		{
			var lightSources = LightService.GetLightSources();
			float maxRank = float.MinValue;
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
		protected virtual void Update()
		{
			FinalTarget = GetTarget();
			OnLookAt?.Invoke(FinalTarget);

			if(FinalTarget == Vector2.zero)
			{
				return;
			}

			if(Vector2.Distance(transform.position, NextWaypoint) < 100f)
			{
				NextWaypoint = PathFindingService.GetNextPosition(transform.position, FinalTarget);
			}
			OnMove?.Invoke(NextWaypoint - (Vector2)transform.position);
			OnAttack?.Invoke();
		}
	}
}