using UnityEngine;

namespace Characters.Player
{
	[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
	public class PlayerData : ScriptableObject
	{
		public int MaxHealth;
		public float Velocity;
		public float AcceleratedVelocity;
		public float AccelerationTime;
		public float AccelerationCooldown;
	}
}