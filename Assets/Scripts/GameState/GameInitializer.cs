using System;
using Characters.Movement;
using Characters;
using Services.Projectile;
using UnityEngine;

namespace GameState
{
	public class GameInitializer : MonoBehaviour
	{
		[SerializeField] private Characters.Character playerCharacter;
		[SerializeField] private PlayerControlsProvider _playerControlsProvider;
		private void Start()
		{
			var gameManager = GameManager.Instance;
			gameManager.RegisterService<IControlsBinder>(new ControlsBinder());
			gameManager.RegisterService<IProjectileService>(new ProjectileService());

			BindPlayerController(gameManager);
		}
		private void BindPlayerController(GameManager gameManager)
		{
			gameManager.GetService<IControlsBinder>().Bind(_playerControlsProvider, playerCharacter);
		}
	}
}