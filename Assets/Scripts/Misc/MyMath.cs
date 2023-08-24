using UnityEngine;

namespace Misc
{
	public static class MyMath
	{
		public static Vector2 SoftNormalize(this Vector2 vector)
		{
			if(vector.sqrMagnitude > 1)
			{
				vector.Normalize();
			}
			return vector;
		}
	}
}