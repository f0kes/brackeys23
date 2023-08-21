using Characters;
using Characters.Movement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services.EnemySpawner
{
	public interface ICharacterSpawner
	{
		void SpawnCharacter(Character prefab, IControlsProvider controlsProvider, Vector2 position, Tilemap tilemap);
	}
}