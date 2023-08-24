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

		private Vector2 _finalTarget;
		private Vector2 _nextWaypoint;

		private IPathFindingService _pathFindingService;
		private ILightService _lightService;
		private void Start()
		{
			var position = transform.position;
			_finalTarget = position;
			_nextWaypoint = position;
			_pathFindingService = GameManager.Instance.GetService<IPathFindingService>();
			_lightService = GameManager.Instance.GetService<ILightService>();
		}
		private Vector2 GetTarget()
		{
			var lightSources = _lightService.GetLightSources();
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
		private void Update()
		{
			_finalTarget = GetTarget();
			OnLookAt?.Invoke(_finalTarget);

			if(_finalTarget == Vector2.zero)
			{
				return;
			}

			if(Vector2.Distance(transform.position, _nextWaypoint) < 100f)
			{
				_nextWaypoint = _pathFindingService.GetNextPosition(transform.position, _finalTarget);
			}
			OnMove?.Invoke(_nextWaypoint - (Vector2)transform.position);
			OnAttack?.Invoke();
		}
	}
}