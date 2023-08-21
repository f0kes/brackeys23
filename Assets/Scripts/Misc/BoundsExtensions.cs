using UnityEngine;

namespace Misc
{
	public static class BoundsExtensions
	{
		public static Vector3Int RandomPosition(this BoundsInt bounds)
		{
			return new Vector3Int(Random.Range(bounds.xMin, bounds.xMax), Random.Range(bounds.yMin, bounds.yMax));
		}
	}
}