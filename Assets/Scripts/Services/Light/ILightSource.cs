﻿using UnityEngine;

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

		float GetIntensity();

		Vector2 GetPosition();

		Vector2 SetPosition(Vector2 position);
	}
}