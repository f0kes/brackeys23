using System;
using Characters.Player;
using GameState;
using Services.MemoryDisplay;
using Services.ProgressionService;
using UnityEngine;

namespace Progression.MemoryItem
{
	public class MemoryItem : MonoBehaviour
	{
		[SerializeField] private int _memoryId;
		[SerializeField] private float _activationRange = 2f;
		[SerializeField] private int _healAmount = 1;
		[SerializeField] [TextArea(7, 20)] private string _memoryText;
		[SerializeField] private AudioClip _memorySound;

		private IProgressionService _progressionService;
		private IMemoryDisplayService _memoryDisplayService;
		private void Start()
		{
			_progressionService = GameManager.Instance.GetService<IProgressionService>();
			_memoryDisplayService = GameManager.Instance.GetService<IMemoryDisplayService>();
			if(_progressionService.GetKeyPoint() != _memoryId) gameObject.SetActive(false);
			_progressionService.OnKeyPointChanged += OnKeyPointChanged;
			_progressionService.RegisterKeyPoint(_memoryId);
		}

		private void OnKeyPointChanged(int obj)
		{
			if(obj != _memoryId) return;
			gameObject.SetActive(true);
		}

		private void Update()
		{
			var player = Player.Instance;
			if(player == null) return;
			var distance = Vector2.Distance(transform.position, player.transform.position);
			if(!(distance < _activationRange)) return;
			if(!Input.GetKeyDown(KeyCode.Space)) return;
			//_progressionService.SetKeyPoint(_progressionService.GetKeyPoint() + 1);
			player.Heal(_healAmount);
			_memoryDisplayService.DisplayMemory(_memoryText, _memorySound);
			_memoryDisplayService.OnHide += OnMemoryDisplayHide;
			_progressionService.OnKeyPointChanged -= OnKeyPointChanged;
			Destroy(gameObject);
		}
		private void OnMemoryDisplayHide()
		{
			_progressionService.SetKeyPoint(_progressionService.GetKeyPoint() + 1);
			_memoryDisplayService.OnHide -= OnMemoryDisplayHide;
		}
		private void OnDestroy()
		{
		}

	}
}