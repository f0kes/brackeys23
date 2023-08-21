using Pathfinding;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services.Pathfinding
{
	public class PathFindingService : IPathFindingService
	{
		private readonly Astar _astar;
		private readonly TilemapToNodeGrid _tilemapConverter;
		private readonly Tilemap _tilemap;
		public PathFindingService(Tilemap colliderTilemap)
		{
			_tilemapConverter = new TilemapToNodeGrid(colliderTilemap);
			_tilemap = colliderTilemap;
			_tilemap.CompressBounds();
			var grid = _tilemapConverter.GenerateGrid();
			_astar = new Astar(grid);
		}
		public Vector2 GetNextPosition(Vector2 startPosition, Vector2 targetPosition)
		{
			var startTileMapPosition = (Vector2Int)_tilemap.WorldToCell(startPosition);
			var targetTileMapPosition = (Vector2Int)_tilemap.WorldToCell(targetPosition);

			var path = _astar.GetPath(startTileMapPosition, targetTileMapPosition);
			if(path == null || path.Count < 2)
			{
				return startPosition;
			}
			var nextNode = path[1];
			var nextPosition = _tilemap.CellToWorld(new Vector3Int(nextNode.X, nextNode.Y, 0));
			for(int i = 0; i < path.Count - 1; i++)
			{
				//Debug.DrawLine(_tilemap.CellToWorld(new Vector3Int(path[i].X, path[i].Y, 0)), _tilemap.CellToWorld(new Vector3Int(path[i + 1].X, path[i + 1].Y, 0)), Color.red, 1f);
			}
			return nextPosition;
		}
	}
}