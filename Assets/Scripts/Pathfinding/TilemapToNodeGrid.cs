using System;
using System.Collections.Generic;
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
		private int _padding;
		private int _penaltyPerWall;

		public TilemapToNodeGrid(Tilemap collider, Tilemap walkable, int padding = 0, int penaltyPerWall = 10)
		{
			_colliderCollider = collider;
			_walkableCollider = walkable;
			_colliderCollider.CompressBounds();
			_walkableCollider.CompressBounds();
			_padding = padding;
			_penaltyPerWall = penaltyPerWall;
			_grid = GenerateGrid();
		}
		private Astar.Node[,] GenerateGrid()
		{
			var size = _colliderCollider.size;
			var grid = new Astar.Node[size.x, size.y];
			var bounds = _colliderCollider.cellBounds;

			for(int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
			{
				for(int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
				{
					var pos = new Vector3Int(x, y, 0);
					if(!IsNodeWalkable(pos))
					{
						grid[i, j] = new Astar.Node(x, y, false);
					}
					else
					{
						grid[i, j] = new Astar.Node(x, y, true, CalculatePenalty(pos, _padding, _penaltyPerWall));
					}
				}
			}
			_grid = grid;
			return grid;
		}
		private bool IsNodeWalkablePadding(Vector3Int position, int padding)
		{
			var x = position.x;
			var y = position.y;
			var neighbors = GetNeighbors(new Vector2Int(x, y), padding);
			if(neighbors.Any(neighbor => !IsNodeWalkable(neighbor))) return false;
			return !_colliderCollider.HasTile(position) && _walkableCollider.HasTile(position);
		}
		private int CalculatePenalty(Vector3Int position, int padding, int penaltyPerWall = 1)
		{
			var x = position.x;
			var y = position.y;
			var neighbors = GetNeighbors(new Vector2Int(x, y), padding);
			var penalty = neighbors.Count(neighbor => !IsNodeWalkable(neighbor)) * penaltyPerWall;
			return penalty;
		}

		private List<Vector3Int> GetNeighbors(Vector2Int position, int distance)
		{
			var bounds = _colliderCollider.cellBounds;
			var neighbors = new List<Vector3Int>();
			for(int x = position.x - distance; x <= position.x + distance; x++)
			{
				for(int y = position.y - distance; y <= position.y + distance; y++)
				{
					if(!IsNodeValid(new Vector2Int(x, y)))
					{
						continue;
					}
					if(x == position.x && y == position.y)
					{
						continue;
					}
					var neighbor = new Vector3Int(x, y, 0);
					neighbors.Add(neighbor);
				}
			}
			return neighbors;
		}
		private bool IsNodeValid(Vector2Int node)
		{
			var bounds = _colliderCollider.cellBounds;
			return node.x >= bounds.xMin && node.x <= bounds.xMax && node.y >= bounds.yMin && node.y <= bounds.yMax;
		}
		private bool IsNodeWalkable(Vector3Int position)
		{
			return !_colliderCollider.HasTile(position) && _walkableCollider.HasTile(position);
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