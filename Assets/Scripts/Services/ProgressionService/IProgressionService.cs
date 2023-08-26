using System;

namespace Services.ProgressionService
{
	public interface IProgressionService
	{
		public event Action<int> OnKeyPointChanged;
		public event Action OnGameEnd;

		int GetKeyPoint();

		void SetKeyPoint(int keyPoint);

		void RegisterKeyPoint(int keyPoint);

	}
}