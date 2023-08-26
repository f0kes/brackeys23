using System;
using Characters.Movement;
using Characters.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Slideshow
{
	public class Slideshow : MonoBehaviour
	{
		[SerializeField] private GameObject _slideshow;
		[SerializeField] private Image _screen;
		[SerializeField] private Sprite[] _slides;

		private int _currentSlideIndex = 0;
		private void Start()
		{
			_currentSlideIndex = 0;
			PlayerControlsProvider.Instance.DisableControls();
		}
		private void NextSlide()
		{
			var nextSlideIndex = _currentSlideIndex + 1;
			if(nextSlideIndex >= _slides.Length)
			{
				Hide();
				return;
			}
			_screen.sprite = _slides[nextSlideIndex];
			_currentSlideIndex = nextSlideIndex;
		}
		private void Hide()
		{
			_slideshow.SetActive(false);
			PlayerControlsProvider.Instance.EnableControls();
		}
		private void Update()
		{
			if(Input.anyKeyDown) NextSlide();
		}

	}
}