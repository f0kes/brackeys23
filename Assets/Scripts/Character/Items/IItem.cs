using Characters.Movement;

namespace Character.Items
{
	public interface IItem
	{
		void Use(ICharacter user);
	}
}