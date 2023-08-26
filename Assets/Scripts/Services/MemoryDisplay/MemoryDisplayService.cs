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
		public event Action OnHide;
		[SerializeField] private GameObject _memoryDisplay;
		[SerializeField] private TextMeshProUGUI _memoryText;
		[SerializeField] private AudioSource _audioSource;
		[SerializeField] private Button _hideButton;
		[SerializeField] private GameObject _tip;

		private Coroutine _coroutine;

		private void Awake()
		{
			_hideButton.onClick.AddListener(HideAndReturnControl);
		}

		private void Start()
		{
			_tip.SetActive(false);
			Hide();
		}

		public void DisplayMemory(string memory, AudioClip audioClip)
		{
			_tip.SetActive(false);
			PlayerControlsProvider.Instance.DisableControls();
			_memoryDisplay.SetActive(true);
			_memoryText.text = memory;
			_audioSource.clip = audioClip;
			_audioSource.Play();
		}

		public void ShowTip()
		{
			if(_coroutine != null)
			{
				StopCoroutine(_coroutine);
			}
			_tip.SetActive(true);
			_coroutine = StartCoroutine(HideTip());
		}

		public IEnumerator HideTip()
		{
			yield return new WaitForSeconds(0.1f);
			_tip.SetActive(false);
		}

		public void HideAndReturnControl()
		{
			StartCoroutine(ReturnControl());
			OnHide?.Invoke();
		}

		public void Hide()
		{
			_memoryDisplay.SetActive(false);
			OnHide?.Invoke();
		}

		private IEnumerator ReturnControl()
		{
			yield return null;
			_memoryDisplay.SetActive(false);
			PlayerControlsProvider.Instance.EnableControls();
		}
	}
}