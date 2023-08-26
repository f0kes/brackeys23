using System;
using GameState;
using Misc;
using Services.ProgressionService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EndScreen
{
	public class EndScreen : MonoBehaviour
	{
		[SerializeField] private GameObject _endScreen;
		[SerializeField] private Image _background;
		[SerializeField] private TextMeshProUGUI _winText;

		[SerializeField] private float _backgroundEasingTime;
		[SerializeField] private float _textEasingTime;
		[SerializeField] private float _pauseTime;

		private IProgressionService _progressionService;

		private enum State
		{
			Inactive,
			EasingBackground,
			Paused,
			EasingText,
			Active
		}
		private State _state = State.Inactive;
		private float _timer = 0f;
		private void Start()
		{
			_endScreen.SetActive(false);
			_progressionService = GameManager.Instance.GetService<IProgressionService>();
			_progressionService.OnGameEnd += OnGameEnd;
			_winText.material = Instantiate(_winText.material);
			_winText.color = Color.clear;
			_background.color = Color.clear;
		}

		private void OnGameEnd()
		{
			_endScreen.SetActive(true);
			SwitchState(State.EasingBackground);
		}
		private void Update()
		{
			_timer += Time.deltaTime;
			switch(_state)
			{
				case State.Inactive:
					HandleIncative();
					break;
				case State.EasingBackground:
					HandleEasingBackground();
					break;
				case State.EasingText:
					HandleEasingText();
					break;
				case State.Active:
					HandleActive();
					break;
				case State.Paused:
					HandlePaused();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void HandleIncative()
		{
		}
		private void HandlePaused()
		{
			if(_timer < _pauseTime) return;
			SwitchState(State.EasingText);
		}
		private void HandleEasingBackground()
		{
			var blend = Tween.EaseOutSine(_timer / _backgroundEasingTime);
			_background.color = Color.Lerp(Color.clear, Color.white, blend);
			if(_timer < _backgroundEasingTime) return;
			SwitchState(State.Paused);
		}
		private void HandleEasingText()
		{
			var blend = Tween.EaseOutSine(_timer / _textEasingTime);
			_winText.color = Color.Lerp(Color.clear, Color.black, blend);
			if(_timer < _textEasingTime) return;
			SwitchState(State.Active);
		}
		private void HandleActive()
		{
		}
		private void SwitchState(State state)
		{
			_state = state;
			_timer = 0f;
		}

	}
}