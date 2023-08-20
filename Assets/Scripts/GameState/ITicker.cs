using System;

namespace GameState
{
	public interface ITicker
	{
		public event Action OnTick;
		public float TickInterval { get; }
		
	}
}