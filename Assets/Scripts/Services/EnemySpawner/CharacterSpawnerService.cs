using Characters;
using Characters.Enemy;
using Characters.Movement;
using GameState;
using Services.Map;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services.EnemySpawner
{
	public class CharacterSpawnerService : ICharacterSpawner
	{

		public void SpawnCharacter(Character prefab, Vector2 position)
		{
			var enemy = Object.Instantiate(prefab, position, Quaternion.identity);
			var controlsProvider = enemy.gameObject.GetComponent<IControlsProvider>() ?? enemy.gameObject.AddComponent<EnemyControlsProvider>();
			GameManager.Instance.GetService<IControlsBinder>().Bind(controlsProvider, enemy);
		}

		public void SpawnCharacter(Character prefab, Vector2 position, float spawnRadius)
		{
			Vector2 randomPosition;
			var i = 0;
			do
			{
				randomPosition = position + Random.insideUnitCircle * spawnRadius;
				i++;
			} while (!GameManager.Instance.GetService<IMapService>().IsWalkable(randomPosition) && i < 100);
			if(i >= 100)
			{
				Debug.LogError("Could not find a walkable position to spawn enemy");
				return;
			}
			SpawnCharacter(prefab, randomPosition);
		}
	}
}