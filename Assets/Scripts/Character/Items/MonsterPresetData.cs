using System;
using UnityEngine;

namespace Characters.Items
{
	[CreateAssetMenu(fileName = "MonsterPresetData", menuName = "Items/MonsterPresetData")]
	public class MonsterPresetData : ScriptableObject
	{
		[Serializable]
		public struct MonsterCountKVP
		{
			public Character Monster;
			public int Count;
		}
		public MonsterCountKVP[] MonsterCounts;
	}
}