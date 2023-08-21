using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

namespace Pathfinding
{
	public class Astar
	{
		public class Node
		{
			public readonly int X;
			public readonly int Y;
			public int G;
			public int H;
			public Node Parent;
			public List<Node> Neighbors = new List<Node>();
			public readonly bool IsWalkable;
			public Node(int x, int y, bool isWalkable = true)
			{
				X = x;
				Y = y;

				IsWalkable = isWalkable;
			}
			public void AddNeighbors(Node[,] grid, int x, int y)
			{
				if(x > 0)
				{
					Neighbors.Add(grid[x - 1, y]);
				}
				if(x < grid.GetLength(0) - 1)
				{
					Neighbors.Add(grid[x + 1, y]);
				}
				if(y > 0)
				{
					Neighbors.Add(grid[x, y - 1]);
				}
				if(y < grid.GetLength(1) - 1)
				{
					Neighbors.Add(grid[x, y + 1]);
				}
				if(x > 0 && y > 0)
				{
					Neighbors.Add(grid[x - 1, y - 1]);
				}
				if(x < grid.GetLength(0) - 1 && y > 0)
				{
					Neighbors.Add(grid[x + 1, y - 1]);
				}
				if(x > 0 && y < grid.GetLength(1) - 1)
				{
					Neighbors.Add(grid[x - 1, y + 1]);
				}
				if(x < grid.GetLength(0) - 1 && y < grid.GetLength(1) - 1)
				{
					Neighbors.Add(grid[x + 1, y + 1]);
				}
			}
		}
		private Node[,] _grid;
		public Astar(Node[,] grid)
		{
			_grid = grid;
			for(var i = 0; i < _grid.GetLength(0); i++)
			{
				for(var j = 0; j < _grid.GetLength(1); j++)
				{
					_grid[i, j].AddNeighbors(_grid, i, j);
				}
			}
		}
		public List<Node> GetPath(Vector2Int start, Vector2Int end)
		{
			var openList = new List<Node>();
			var closedList = new List<Node>();

			var startNode = FindNodeWithPosition(start);
			var endNode = FindNodeWithPosition(end);
			if(startNode == null || endNode == null)
			{
				Debug.Log(start.x + " " + start.y + " " + end.x + " " + end.y);
				return null;
			}

			openList.Add(startNode);
			while (openList.Count > 0)
			{
				var currentNode = openList[0];
				for(int i = 1; i < openList.Count; i++)
				{
					if(openList[i].G + openList[i].H < currentNode.G + currentNode.H)
					{
						currentNode = openList[i];
					}
				}
				openList.Remove(currentNode);
				closedList.Add(currentNode);
				if(currentNode == endNode)
				{
					return GetFinalPath(startNode, endNode);
				}
				foreach(var neighbor in currentNode.Neighbors)
				{
					if(!neighbor.IsWalkable || closedList.Contains(neighbor))
					{
						continue;
					}
					var tentativeG = currentNode.G + GetDistance(currentNode, neighbor);
					if(tentativeG < neighbor.G || !openList.Contains(neighbor))
					{
						neighbor.G = tentativeG;
						neighbor.H = GetDistance(neighbor, endNode);
						neighbor.Parent = currentNode;
						if(!openList.Contains(neighbor))
						{
							openList.Add(neighbor);
						}
					}
				}
			}
			return null;
		}
		private Node FindNodeWithPosition(Vector2Int position)
		{
			return _grid.Cast<Node>().FirstOrDefault(node => node.X == position.x && node.Y == position.y);
		}
		private int GetDistance(Node currentNode, Node neighbor)
		{
			var distanceX = Mathf.Abs(currentNode.X - neighbor.X);
			var distanceY = Mathf.Abs(currentNode.Y - neighbor.Y);
			if(distanceX > distanceY)
			{
				return 14 * distanceY + 10 * (distanceX - distanceY);
			}
			return 14 * distanceX + 10 * (distanceY - distanceX);
		}

		private List<Node> GetFinalPath(Node startNode, Node endNode)
		{
			var finalPath = new List<Node>();
			var currentNode = endNode;
			while (currentNode != startNode)
			{
				finalPath.Add(currentNode);
				currentNode = currentNode.Parent;
			}
			finalPath.Reverse();
			return finalPath;
		}

	}
}