using UnityEngine;

namespace Progression
{
	[CreateAssetMenu(fileName = "LightProgression", menuName = "Progression/LightProgression")]
	public class LightProgression : ScriptableObject
	{
		public float MaxBrightness;
		public float DeltaBrightness;
	}
}