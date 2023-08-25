using System;

namespace Services.ProgressionService
{
	public interface IProgressionService
	{
		public event Action<int> OnKeyPointChanged; 
		int GetKeyPoint();

		void SetKeyPoint(int keyPoint);
	}
}