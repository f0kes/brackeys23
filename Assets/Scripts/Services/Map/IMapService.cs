using System.Collections.Generic;
using UnityEngine;

namespace Services.Map
{
	public interface IMapService
	{
		List<Vector2> GetEmptyTiles();

		bool IsWalkable(Vector2 position);

		Vector2 GetRandomEmptyTile();

		List<Vector2> GetEmptyTilesAround(Vector2 position, float radius);

	}
}