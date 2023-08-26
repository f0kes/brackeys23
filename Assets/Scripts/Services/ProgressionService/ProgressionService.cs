using System;
using Progression;
using Services.Light;

namespace Services.ProgressionService
{
	public class ProgressionService : IProgressionService
	{
		private int _keyPoint = 0;
		private readonly LightProgression _lightProgression;
		private readonly ILightService _lightService;
		public event Action<int> OnKeyPointChanged;

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
		}

		public void AddLight()
		{
			_lightService.SetAmbientLightIntensity(_lightService.GetAmbientLightIntensity() + _lightProgression.DeltaBrightness);
		}
	}
}