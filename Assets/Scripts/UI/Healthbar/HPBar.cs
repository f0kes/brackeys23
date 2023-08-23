using System;
using System.Collections;
using System.Collections.Generic;
using Characters;
using Characters.Player;
using UnityEngine;

namespace UI
{
	public class HPBar : MonoBehaviour
	{
		[SerializeField] private HPSegment _segmentPrefab;
		[SerializeField] private float _animationDelay = 0.1f;
		private IDamagable _damagable;

		private List<HPSegment> _allSegments = new();
		private LinkedList<HPSegment> _toActivate = new();
		private LinkedList<HPSegment> _toDeactivate = new();

		private int _previousHealth;
		private void Start()
		{
			//todo:remove
			SetDamagable(Player.Instance);
		}

		public void SetDamagable(IDamagable damagable)
		{
			_damagable = damagable;
			_damagable.OnHealthChanged += OnHealthChanged;
			_previousHealth = _damagable.GetHealth();
			for(int i = 0; i < _damagable.GetHealth(); i++)
			{
				var instance = Instantiate(_segmentPrefab, transform);
				_toActivate.AddLast(instance);
				_allSegments.Add(instance);
			}
			StartCoroutine(AddMultiple(_previousHealth));
		}

		private void OnHealthChanged(int newHealth)
		{
			var diff = newHealth - _previousHealth;
			_previousHealth = newHealth;
			switch(diff)
			{
				case 0:
					return;
				case > 0:
					StartCoroutine(AddMultiple(diff));
					break;
				case < 0:
					StartCoroutine(RemoveMultiple(-diff));
					break;
			}
		}
		private IEnumerator AddMultiple(int count)
		{
			for(int i = 0; i < count; i++)
			{
				while (_toActivate.Count == 0)
				{
					yield return null;
				}
				var segment = _toActivate.First.Value;
				_toActivate.RemoveFirst();
				ActivateHPSegment(segment);
				_toDeactivate.AddLast(segment);
			}
		}

		private void ActivateHPSegment(HPSegment segment)
		{
			segment.Activate();
		}

		private IEnumerator RemoveMultiple(int count)
		{
			for(var i = 0; i < count; i++)
			{
				while (_toDeactivate.Count == 0)
				{
					yield return null;
				}
				var segment = _toDeactivate.Last.Value;
				_toDeactivate.RemoveLast();
				DeactivateHPSegment(segment);
				_toActivate.AddFirst(segment);
			}
		}

		private void DeactivateHPSegment(HPSegment segment)
		{
			segment.Deactivate();
		}

	}
}