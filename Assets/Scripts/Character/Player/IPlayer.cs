using System;
using Services.Light;

namespace Characters.Player
{
	public interface IPlayer
	{
		public event Action OnStartRunning;
		public event Action OnStopRunning;

		void SetPlayerData(PlayerData playerData);

		ILightSource GetBlinkSource();
	}
}