using System;
using GameState;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Services.Light
{
	[RequireComponent(typeof(Light2D))]
	public class LightSource : MonoBehaviour, ILightSource
	{
		private Light2D _light2D;
		private void Awake()
		{
			_light2D = GetComponent<Light2D>();
		}
		private void Start()
		{
			GameManager.Instance.GetService<ILightService>().RegisterLightSource(this);
		}
		public void SetIntensity(float intensity)
		{
			_light2D.intensity = intensity;
		}

		public void SetColor(Color color)
		{
			_light2D.color = color;
		}

		public void SetInnerRadius(float innerRadius)
		{
			_light2D.pointLightInnerRadius = innerRadius;
		}

		public void SetOuterRadius(float outerRadius)
		{
			_light2D.pointLightOuterRadius = outerRadius;
		}

		public void SetFallOff(float fallOff)
		{
			
		}

		public void SetShimmer(float shimmer)
		{
			
		}
	}
}