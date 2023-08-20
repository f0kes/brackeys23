namespace Character
{
	public interface IControlsBinder
	{
		void Bind(IControlsProvider controlsProvider, IMover mover);
	}
}