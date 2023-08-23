using System;
using UnityEngine;

namespace Characters.Items
{
	[CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData")]
	public class ItemData : ScriptableObject
	{
		[Serializable] public struct MonsterProgression
		{
			public int KeyPoint;
			public MonsterPresetData MonsterPreset;
		}
		public float Cooldown;
		public float SpawnTimer;
		public float SpawnRadius;
		public Vector2 SpawnPoint;
		public MonsterProgression[] MonsterProgressions;
	}
}