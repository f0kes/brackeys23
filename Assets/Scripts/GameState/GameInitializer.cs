using System;
using Characters.Movement;
using Characters;
using Characters.Enemy;
using Misc;
using Services.EnemySpawner;
using Services.Light;
using Services.Pathfinding;
using Services.Projectile;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace GameState
{
	public class GameInitializer : MonoBehaviour
	{
		[SerializeField] private Character playerCharacter;
		[SerializeField] private Character enemyCharacter;
		[SerializeField] private int enemyCount;
		[SerializeField] private PlayerControlsProvider _playerControlsProvider;

		[SerializeField] private Tilemap _walkableTilemap;
		[SerializeField] private Tilemap _colliderTilemap;
		private void Awake()
		{
			var gameManager = new GameManager();
			SceneManager.sceneUnloaded += (scene) => gameManager.Dispose();

			gameManager.RegisterService<IControlsBinder>(new ControlsBinder());
			gameManager.RegisterService<IProjectileService>(new ProjectileService());
			gameManager.RegisterService<ILightService>(new LightService());
			gameManager.RegisterService<IPathFindingService>(new PathFindingService(_colliderTilemap));
			gameManager.RegisterService<ICharacterSpawner>(new CharacterSpawnerService());

			BindPlayerController(gameManager);
			SpawnEnemies();
		}
		private void BindPlayerController(GameManager gameManager)
		{
			gameManager.GetService<IControlsBinder>().Bind(_playerControlsProvider, playerCharacter);
		}
		private void SpawnEnemies()
		{
			var characterSpawner = GameManager.Instance.GetService<ICharacterSpawner>();
			for(int i = 0; i < enemyCount; i++)
			{
				var position = (Vector2)_walkableTilemap.GetCellCenterWorld(_colliderTilemap.cellBounds.RandomPosition());
				var enemy = Instantiate(enemyCharacter, position, Quaternion.identity);
				var controlsProvider = enemy.gameObject.AddComponent<EnemyControlsProvider>();
				GameManager.Instance.GetService<IControlsBinder>().Bind(controlsProvider, enemy);
			}
		}
	}
}