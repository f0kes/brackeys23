using System.Collections;
using Misc;
using Services.Light;
using UnityEngine;

namespace Characters.Player
{
	public class PlayerLight : LightSource
	{
		[SerializeField] private float _blinkIntensity;
		[SerializeField] private float _blinkDuration;
		[SerializeField] private float _baseIntensity;
		private bool _isBlinking;
		public void Blink()
		{
			if(_isBlinking)
			{
				StopAllCoroutines();
				_isBlinking = false;
			}
			StartCoroutine(BlinkCoroutine());
		}
		private IEnumerator BlinkCoroutine()
		{
			var time = 0f;
			_isBlinking = true;

			while (time < _blinkDuration)
			{
				var blend = Tween.EaseOutSine(time / _blinkDuration);
				SetIntensity(Mathf.Lerp(_blinkIntensity, _baseIntensity, blend));
				//SetOuterRadius(Mathf.Lerp(_blinkIntensity, 0, blend));
				time += Time.deltaTime;
				yield return null;
			}
		}
	}
}