using System;
using Characters.Player;
using GameState;
using Services.ProgressionService;
using UnityEngine;

namespace Progression
{
	[RequireComponent(typeof(Collider2D))]
	public class Checkpoint : MonoBehaviour
	{
		private IProgressionService _progressionService;
		private void Start()
		{
			_progressionService = GameManager.Instance.GetService<IProgressionService>();
		}
		private void OnTriggerEnter2D(Collider2D col)
		{
			if(col.gameObject.GetComponent<Player>() == null) return;
			_progressionService.SetKeyPoint(_progressionService.GetKeyPoint() + 1);
			Destroy(gameObject);
		}
	}
}