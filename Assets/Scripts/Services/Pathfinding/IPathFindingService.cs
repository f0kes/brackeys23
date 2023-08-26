using System.Collections.Generic;
using Pathfinding;
using UnityEditor;
using UnityEngine;

namespace Services.Pathfinding
{
	public interface IPathFindingService
	{
		Vector2 GetNextPosition(Vector2 startPosition, Vector2 targetPosition);

		List<Astar.Node> GetPath(Vector2 startPosition, Vector2 targetPosition);

		Vector2 GetNextPosition(List<Astar.Node> path, int currentNodeIndex);

		float GetPathRequestTimeOffset();
	}
}