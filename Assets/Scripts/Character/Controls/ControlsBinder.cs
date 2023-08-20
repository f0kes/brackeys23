namespace Characters.Movement
{
	public class ControlsBinder : IControlsBinder
	{
		public void Bind(IControlsProvider controlsProvider, ICharacter character)
		{
			controlsProvider.OnMove += character.Move;
			controlsProvider.OnLookAt += character.LookAt;
			controlsProvider.OnChangeItem += character.ChangeItem;
			controlsProvider.OnUseItem += character.UseItem;
		}
	}
}