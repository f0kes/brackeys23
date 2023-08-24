using System;
using System.Collections;
using System.Linq;
using Characters.Movement;
using GameState;
using Services.EnemySpawner;
using Services.ProgressionService;
using UnityEngine;

namespace Characters.Items
{
	public abstract class Item : MonoBehaviour, IItem
	{
		[SerializeField] private ItemData _itemData;

		private float _cooldownTimer;


		public virtual void Use(ICharacter user)
		{
			_cooldownTimer = _itemData.Cooldown;
			StartCoroutine(SpawnEnemies(user));
		}
		public virtual Vector2 GetAnchor(ICharacter user)
		{
			return transform.position;
		}

		public bool IsReady()
		{
			return _cooldownTimer <= 0;
		}

		public virtual void Update()
		{
			_cooldownTimer -= Time.deltaTime;
		}
		private IEnumerator SpawnEnemies(ICharacter character)
		{
			yield return new WaitForSeconds(_itemData.SpawnTimer);
			var gameManager = GameManager.Instance;
			var spawnService = gameManager.GetService<ICharacterSpawner>();
			var progression = gameManager.GetService<IProgressionService>().GetKeyPoint();
			var enemies = _itemData.MonsterProgressions.FirstOrDefault(x => x.KeyPoint == progression).MonsterPreset.MonsterCounts;
			var position = _itemData.SpawnPoint + GetAnchor(character);
			//Debug.DrawLine((position - Vector2.up * 0.5f), position + Vector2.up * 0.5f, Color.red, 5f);
			//Debug.DrawLine((position - Vector2.right * 0.5f), position + Vector2.right * 0.5f, Color.red, 5f);
			var radius = _itemData.SpawnRadius;
			foreach(var kvp in enemies)
			{
				for(var i = 0; i < kvp.Count; i++)
				{
					spawnService.SpawnCharacter(kvp.Monster, position, radius);
				}
			}
		}
	}
}