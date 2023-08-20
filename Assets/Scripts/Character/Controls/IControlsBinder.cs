namespace Characters.Movement
{
	public interface IControlsBinder
	{
		void Bind(IControlsProvider controlsProvider, ICharacter character);
	}
}