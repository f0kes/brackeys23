using System;
using Character;
using UnityEngine;

namespace GameState
{
	public class GameInitializer : MonoBehaviour
	{
		[SerializeField] private CharacterMover _playerMover;
		[SerializeField] private PlayerControlsProvider _playerControlsProvider;
		private void Start()
		{
			var gameManager = GameManager.Instance;
			gameManager.RegisterService<IControlsBinder>(new ControlsBinder());

			BindPlayerController(gameManager);
		}
		private void BindPlayerController(GameManager gameManager)
		{
			gameManager.GetService<IControlsBinder>().Bind(_playerControlsProvider, _playerMover);
		}
	}
}