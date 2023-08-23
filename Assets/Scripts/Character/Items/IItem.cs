using Characters.Movement;

namespace Characters.Items
{
	public interface IItem
	{
		void Use(ICharacter user);
		bool IsReady();
	}
}