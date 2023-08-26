using System;
using Characters.Movement;
using Characters;
using Characters.Enemy;
using Characters.Player;
using Misc;
using Pathfinding;
using Progression;
using Services.EnemySpawner;
using Services.ItemUse;
using Services.Light;
using Services.Map;
using Services.MemoryDisplay;
using Services.Pathfinding;
using Services.ProgressionService;
using Services.Projectile;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace GameState
{
	public class GameInitializer : MonoBehaviour
	{
		public static event Action OnGameInitialized;

		[SerializeField] private Character playerCharacter;
		[SerializeField] private Character enemyCharacter;
		[SerializeField] private int enemyCount;
		[SerializeField] private PlayerControlsProvider _playerControlsProvider;

		[SerializeField] private Tilemap _walkableTilemap;
		[SerializeField] private Tilemap _colliderTilemap;
		[SerializeField] private LightProgression _lightProgression;
		[SerializeField] private MemoryDisplayService _memoryDisplayService;

		private void Awake()
		{
			var gameManager = new GameManager();
			SceneManager.sceneUnloaded += (scene) => gameManager.Dispose();

			gameManager.RegisterService<IControlsBinder>(new ControlsBinder());
			gameManager.RegisterService<IProjectileService>(new ProjectileService());
			var lightService = new LightService();
			gameManager.RegisterService<ILightService>(lightService);
			lightService.SetAmbientLightIntensity(_lightProgression.MaxBrightness);

			var mapGenerator = new TilemapToNodeGrid(_colliderTilemap, _walkableTilemap, 1);
			gameManager.RegisterService<IPathFindingService>(new PathFindingService(mapGenerator));
			gameManager.RegisterService<IMapService>(new MapService(mapGenerator));

			gameManager.RegisterService<ICharacterSpawner>(new CharacterSpawnerService());
			gameManager.RegisterService<IProgressionService>(new ProgressionService(_lightProgression, lightService));
			gameManager.RegisterService<IMemoryDisplayService>(_memoryDisplayService);


			var itemUseService = ItemUseServiceFactory.Create();
			gameManager.RegisterService(itemUseService);

			BindPlayerController(gameManager);
			OnGameInitialized?.Invoke();
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