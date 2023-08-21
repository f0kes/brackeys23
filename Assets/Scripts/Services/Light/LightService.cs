using System.Collections.Generic;
using UnityEngine;

namespace Services.Light
{
	public class LightService : ILightService
	{
		private List<ILightSource> _lightSources = new List<ILightSource>();

		public void RegisterLightSource(ILightSource lightSource)
		{
			_lightSources.Add(lightSource);
		}

		public void UnregisterLightSource(ILightSource lightSource)
		{
			_lightSources.Remove(lightSource);
		}

		public void SetGlobalLightIntensity(float intensity)
		{
			foreach(var lightSource in _lightSources)
			{
				lightSource.SetIntensity(intensity);
			}
		}

		public void SetGlobalLightColor(Color color)
		{
			foreach(var lightSource in _lightSources)
			{
				lightSource.SetColor(color);
			}
		}

		public void SetGlobalShimmer(float shimmer)
		{
			foreach(var lightSource in _lightSources)
			{
				lightSource.SetShimmer(shimmer);
			}
		}

		public void SetGlobalFallOff(float fallOff)
		{
			foreach(var lightSource in _lightSources)
			{
				lightSource.SetFallOff(fallOff);
			}
		}

		public void SetGlobalInnerRadius(float innerRadius)
		{
			foreach(var lightSource in _lightSources)
			{
				lightSource.SetInnerRadius(innerRadius);
			}
		}

		public void SetGlobalOuterRadius(float outerRadius)
		{
			foreach(var lightSource in _lightSources)
			{
				lightSource.SetOuterRadius(outerRadius);
			}
		}
	}
}