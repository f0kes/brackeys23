using Characters;
using Characters.Movement;
using GameState;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Services.EnemySpawner
{
	public class CharacterSpawnerService : ICharacterSpawner
	{

		public void SpawnCharacter(Character prefab, IControlsProvider controlsProvider, Vector2 position, Tilemap tilemap)
		{
			var character = Object.Instantiate(prefab, position, Quaternion.identity);
			var controlsBinder = GameManager.Instance.GetService<IControlsBinder>();
			controlsBinder.Bind(controlsProvider, character);
		}
	}
}