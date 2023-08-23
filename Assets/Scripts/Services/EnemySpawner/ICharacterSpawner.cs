using Characters;
using Characters.Movement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services.EnemySpawner
{
	public interface ICharacterSpawner
	{
		void SpawnCharacter(Character prefab, Vector2 position);

		void SpawnCharacter(Character prefab, Vector2 position, float spawnRadius);
	}
}