using System;
using Progression;
using Services.Light;

namespace Services.ProgressionService
{
	public class ProgressionService : IProgressionService
	{
		private int _keyPoint = 0;
		private int _maxKeyPoint = 0;
		private readonly LightProgression _lightProgression;
		private readonly ILightService _lightService;
		public event Action<int> OnKeyPointChanged;
		public event Action OnGameEnd;

		public ProgressionService(LightProgression lightProgression, ILightService lightService)
		{
			_lightProgression = lightProgression;
			_lightService = lightService;
		}

		public int GetKeyPoint()
		{
			return _keyPoint;
		}

		public void SetKeyPoint(int keyPoint)
		{
			var diff = keyPoint - _keyPoint;
			_keyPoint = keyPoint;
			_lightService.SetAmbientLightIntensity(_lightService.GetAmbientLightIntensity() - diff * _lightProgression.DeltaBrightness);
			OnKeyPointChanged?.Invoke(_keyPoint);
			if(_keyPoint == _maxKeyPoint + 1) OnGameEnd?.Invoke();
		}

		public void RegisterKeyPoint(int keyPoint)
		{
			_maxKeyPoint = Math.Max(_maxKeyPoint, keyPoint);
		}

		public void AddLight()
		{
			_lightService.SetAmbientLightIntensity(_lightService.GetAmbientLightIntensity() - _lightProgression.DeltaBrightness);
		}
	}
}