using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using UnityEngine;


namespace Services.Map
{
	public class MapService : IMapService
	{
		private readonly IMapGenerator _mapGenerator;
		private readonly Astar.Node[,] _grid;
		public MapService(IMapGenerator mapGenerator)
		{
			_mapGenerator = mapGenerator;
			_grid = _mapGenerator.GetGrid();
		}

		public List<Vector2> GetEmptyTiles()
		{
			return (from Astar.Node node in _grid where node.IsWalkable select _mapGenerator.CastToWorldPosition(new Vector2Int(node.X, node.Y))).ToList();
		}

		public bool IsWalkable(Vector2 position)
		{
			var node = _mapGenerator.GetNodeAtPosition(position);
			return node is { IsWalkable: true };
		}

		public Vector2 GetRandomEmptyTile()
		{
			var emptyTiles = GetEmptyTiles();
			return emptyTiles[Random.Range(0, emptyTiles.Count)];
		}

		public List<Vector2> GetEmptyTilesAround(Vector2 position, float radius)
		{
			var gridPosition = _mapGenerator.CastToTilemapPosition(position);
			var emptyTiles = new List<Vector2>();
			for(float x = gridPosition.x - (int)radius; x < gridPosition.x + radius; x++)
			{
				for(float y = gridPosition.y - (int)radius; y < gridPosition.y + radius; y++)
				{
					if(x < 0 || y < 0 || x >= _grid.GetLength(0) || y >= _grid.GetLength(1))
					{
						continue;
					}
					var intPosition = new Vector2Int((int)x, (int)y);
					if(_mapGenerator.GetNodeAtPosition(intPosition).IsWalkable)
					{
						emptyTiles.Add(_mapGenerator.CastToWorldPosition(intPosition));
					}
				}
			}
			return emptyTiles;
		}
	}
}