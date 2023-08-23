using System;
using System.Linq;
using Services.Map;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Pathfinding
{
	public class TilemapToNodeGrid : IMapGenerator
	{
		private Tilemap _colliderTilemap;
		private Astar.Node[,] _grid;

		public TilemapToNodeGrid(Tilemap tilemap)
		{
			_colliderTilemap = tilemap;
			_colliderTilemap.CompressBounds();
			_grid = GenerateGrid();
		}
		public Astar.Node[,] GenerateGrid()
		{
			var size = _colliderTilemap.size;
			var grid = new Astar.Node[size.x, size.y];
			var bounds = _colliderTilemap.cellBounds;
			for(int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
			{
				for(int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
				{
					if(_colliderTilemap.HasTile(new Vector3Int(x, y, 0)))
					{
						grid[i, j] = new Astar.Node(x, y, false);
					}
					else
					{
						grid[i, j] = new Astar.Node(x, y);
					}
				}
			}
			_grid = grid;
			return grid;
		}

		public Astar.Node[,] GetGrid()
		{
			return _grid;
		}

		public Astar.Node GetNodeAtPosition(Vector2 position)
		{
			var gridPosition = CastToTilemapPosition(position);
			return _grid.Cast<Astar.Node>().FirstOrDefault(node => node.X == gridPosition.x && node.Y == gridPosition.y);
		}

		public Vector2Int CastToTilemapPosition(Vector2 position)
		{
			return (Vector2Int)_colliderTilemap.WorldToCell(position);
		}

		public Vector2 CastToWorldPosition(Vector2Int position)
		{
			return _colliderTilemap.CellToWorld((Vector3Int)position);
		}
	}
}