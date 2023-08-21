namespace Services.Light
{
	public interface ILightService
	{
		void RegisterLightSource(ILightSource lightSource);

		void UnregisterLightSource(ILightSource lightSource);

		void SetGlobalLightIntensity(float intensity);

		void SetGlobalLightColor(UnityEngine.Color color);

		void SetGlobalShimmer(float shimmer);

		void SetGlobalFallOff(float fallOff);

		void SetGlobalInnerRadius(float innerRadius);

		void SetGlobalOuterRadius(float outerRadius);
	}
}