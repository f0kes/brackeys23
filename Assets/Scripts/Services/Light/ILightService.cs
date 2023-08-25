using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Light
{
	public interface ILightService
	{
		void RegisterLightSource(ILightSource lightSource);

		void RegisterAmbientLightSource(ILightSource ambientLightSource);

		void UnregisterLightSource(ILightSource lightSource);

		void UnregisterAmbientLightSource(ILightSource ambientLightSource);

		void SetGlobalLightIntensity(float intensity);

		void SetGlobalLightColor(UnityEngine.Color color);

		void SetGlobalShimmer(float shimmer);

		void SetGlobalFallOff(float fallOff);

		void SetGlobalInnerRadius(float innerRadius);

		void SetGlobalOuterRadius(float outerRadius);

		void ExecuteLightEvent(Vector2 position);

		List<ILightSource> GetLightSources();

		void SetAmbientLightIntensity(float intensity);
		float GetAmbientLightIntensity();

		event Action<Vector2> OnLightEvent;
	}
}