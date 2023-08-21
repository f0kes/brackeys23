using System;
using Characters.Movement;
using GameState;
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

		private Vector2 _finalTarget;
		private Vector2 _nextWaypoint;

		private IPathFindingService _pathFindingService;
		private void Start()
		{
			var position = transform.position;
			_finalTarget = position;
			_nextWaypoint = position;
			_pathFindingService = GameManager.Instance.GetService<IPathFindingService>();
		}
		private void Update()
		{
			_finalTarget = Player.Player.Instance.transform.position; //TODO: change target dynamically
			if(Vector2.Distance(transform.position, _nextWaypoint) < 100f)
			{
				_nextWaypoint = _pathFindingService.GetNextPosition(transform.position, _finalTarget);
			}
			OnMove?.Invoke(_nextWaypoint - (Vector2)transform.position);
			OnLookAt?.Invoke(_finalTarget);
			OnAttack?.Invoke();
		}
	}
}