using UnityEngine;

namespace Services.Pathfinding
{
	public interface IPathFindingService
	{
		Vector2 GetNextPosition(Vector2 startPosition, Vector2 targetPosition);
	}
}