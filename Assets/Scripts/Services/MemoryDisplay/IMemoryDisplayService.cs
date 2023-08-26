﻿using UnityEngine;

namespace Services.MemoryDisplay
{
	public interface IMemoryDisplayService
	{
		void DisplayMemory(string memory, AudioClip audioClip);

		void ShowTip();
	}
}