using Pathfinding;
using UnityEngine;

namespace Services.Map
{
	public interface IMapGenerator
	{
		Astar.Node[,] GetGrid();

		Astar.Node GetNodeAtPosition(Vector2 position);

		Vector2Int CastToTilemapPosition(Vector2 position);

		Vector2 CastToWorldPosition(Vector2Int position);
	}
}