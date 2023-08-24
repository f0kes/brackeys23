using Pathfinding;
using Services.Map;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services.Pathfinding
{
	public class PathFindingService : IPathFindingService
	{
		private readonly Astar _astar;
		private readonly IMapGenerator _tilemapConverter;

		public PathFindingService(IMapGenerator mapGenerator)
		{
			_tilemapConverter = mapGenerator;
			var grid = _tilemapConverter.GetGrid();
			_astar = new Astar(grid);
		}
		public Vector2 GetNextPosition(Vector2 startPosition, Vector2 targetPosition)
		{
			var startTileMapPosition = _tilemapConverter.CastToTilemapPosition(startPosition);
			var targetTileMapPosition = _tilemapConverter.CastToTilemapPosition(targetPosition);
			
			Debug.Log(startTileMapPosition + " from");
			Debug.Log(targetTileMapPosition+ " to");
			

			var path = _astar.GetPath(startTileMapPosition, targetTileMapPosition);
			if(path == null || path.Count < 2)
			{
				return startPosition;
			}
			var nextNode = path[1];
			var nextPosition = _tilemapConverter.CastToWorldPosition(new Vector2Int(nextNode.X, nextNode.Y));
			for(int i = 0; i < path.Count - 1; i++)
			{
				Debug.DrawLine(_tilemapConverter.CastToWorldPosition(new Vector2Int(path[i].X, path[i].Y)),
					_tilemapConverter.CastToWorldPosition(new Vector2Int(path[i + 1].X, path[i + 1].Y)), Color.red, 1f);
			}
			return nextPosition;
		}
	}
}