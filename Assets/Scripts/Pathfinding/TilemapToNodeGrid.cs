using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pathfinding
{
	public class TilemapToNodeGrid
	{
		private Tilemap _colliderTilemap;

		public TilemapToNodeGrid(Tilemap tilemap)
		{
			_colliderTilemap = tilemap;
			_colliderTilemap.CompressBounds();
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
			return grid;
		}
	}
}