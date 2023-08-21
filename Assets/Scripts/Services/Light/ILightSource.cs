namespace Services.Light
{
	public interface ILightSource
	{
		void SetIntensity(float intensity);

		void SetColor(UnityEngine.Color color);

		void SetInnerRadius(float innerRadius);

		void SetOuterRadius(float outerRadius);

		void SetFallOff(float fallOff);

		void SetShimmer(float shimmer);
	}
}