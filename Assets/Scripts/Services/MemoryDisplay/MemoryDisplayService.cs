using System;
using System.Collections;
using Characters.Movement;
using GameState;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.MemoryDisplay
{
	public class MemoryDisplayService : MonoBehaviour, IMemoryDisplayService
	{
		[SerializeField] private GameObject _memoryDisplay;
		[SerializeField] private TextMeshProUGUI _memoryText;
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private Button _hideButton;

		private void Awake()
		{
			_hideButton.onClick.AddListener(Hide);
			//GameInitializer.OnGameInitialized += RegisterService;
		}
		private void RegisterService()
		{
			GameManager.Instance.RegisterService<IMemoryDisplayService>(this, true);
			GameInitializer.OnGameInitialized -= RegisterService;
		}
		private void Start()
		{
			Hide();
		}
		public void DisplayMemory(string memory, AudioClip audioClip)
		{
			PlayerControlsProvider.Instance.DisableControls();
			_memoryDisplay.SetActive(true);
			_memoryText.text = memory;
			_audioSource.clip = audioClip;
			_audioSource.Play();
		}
		public void Hide()
		{
			StartCoroutine(ReturnControl());
		}

		private IEnumerator ReturnControl()
		{
			yield return null;
			_memoryDisplay.SetActive(false);
			PlayerControlsProvider.Instance.EnableControls();
		}
	}
}