using System;
using System.Linq;
using Services.Map;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Pathfinding
{
	public class TilemapToNodeGrid : IMapGenerator
	{
		private Tilemap _colliderCollider;
		private Tilemap _walkableCollider;
		private Astar.Node[,] _grid;

		public TilemapToNodeGrid(Tilemap collider, Tilemap walkable)
		{
			_colliderCollider = collider;
			_walkableCollider = walkable;
			_colliderCollider.CompressBounds();
			_walkableCollider.CompressBounds();
			_grid = GenerateGrid();
		}
		public Astar.Node[,] GenerateGrid()
		{
			var size = _colliderCollider.size;
			var grid = new Astar.Node[size.x, size.y];
			var bounds = _colliderCollider.cellBounds;
			for(int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
			{
				for(int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
				{
					if(_colliderCollider.HasTile(new Vector3Int(x, y, 0)) || !_walkableCollider.HasTile(new Vector3Int(x, y, 0)))
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
			return (Vector2Int)_colliderCollider.WorldToCell(position);
		}

		public Vector2 CastToWorldPosition(Vector2Int position)
		{
			return _colliderCollider.CellToWorld((Vector3Int)position);
		}
	}
}