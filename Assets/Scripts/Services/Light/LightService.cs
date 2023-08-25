using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Light
{
	public class LightService : ILightService
	{
		public event Action<Vector2> OnLightEvent;
		private List<ILightSource> _lightSources = new List<ILightSource>();
		private ILightSource _ambientLightSource;
		private float _ambientLightIntensity = 0.5f;


		public void RegisterLightSource(ILightSource lightSource)
		{
			_lightSources.Add(lightSource);
		}

		public void RegisterAmbientLightSource(ILightSource ambientLightSource)
		{
			_ambientLightSource = ambientLightSource;
			_ambientLightSource.SetIntensity(_ambientLightIntensity);
		}

		public void UnregisterLightSource(ILightSource lightSource)
		{
			_lightSources.Remove(lightSource);
		}

		public void UnregisterAmbientLightSource(ILightSource ambientLightSource)
		{
			_ambientLightSource = null;
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

		public void ExecuteLightEvent(Vector2 position)
		{
			OnLightEvent?.Invoke(position);
		}

		public List<ILightSource> GetLightSources()
		{
			return _lightSources;
		}

		public void SetAmbientLightIntensity(float intensity)
		{
			_ambientLightIntensity = intensity;
			_ambientLightSource?.SetIntensity(intensity);
		}

		public float GetAmbientLightIntensity()
		{
			return _ambientLightSource.GetIntensity();
		}
	}
}