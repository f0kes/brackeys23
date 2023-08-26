using System;
using UnityEngine;

namespace Services.MemoryDisplay
{
	public interface IMemoryDisplayService
	{
		public event Action OnHide;

		void DisplayMemory(string memory, AudioClip audioClip);

		void ShowTip();

	}
}