namespace Character
{
	public class ControlsBinder : IControlsBinder
	{
		public void Bind(IControlsProvider controlsProvider, IMover mover)
		{
			controlsProvider.OnMove += mover.Move;
			controlsProvider.OnLookAt += mover.LookAt;
		}
	}
}