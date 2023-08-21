using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class HPSegment : MonoBehaviour
	{
		[SerializeField] private Image _fill; 
		public void Activate()
		{
			_fill.enabled = true;
		}
		public void Deactivate()
		{
			_fill.enabled = false;
		}
	}
}