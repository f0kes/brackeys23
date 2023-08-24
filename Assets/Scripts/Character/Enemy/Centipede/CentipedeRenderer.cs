using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Characters.Enemy.Centipede
{
	public class CentipedeRenderer : MonoBehaviour
	{
		[SerializeField] private GameObject _segmentPrefab;
		[SerializeField] private GameObject _assPrefab;
		[SerializeField] private int _segmentsCount;
		[SerializeField] private float _targetDistance;
		[SerializeField] private Transform _targetDirection;
		[SerializeField] private float _smoothSpeed;
		[SerializeField] private Transform _target;

		private Vector3[] _segmentsPositions;
		private Vector3[] _previousSegmentsPositions;
		private Vector3[] _segmentVelocities;
		private GameObject[] _segments;

		private void Start()
		{
			_segments = new GameObject[_segmentsCount];
			_segmentsPositions = new Vector3[_segmentsCount];
			_segmentVelocities = new Vector3[_segmentsCount];
			_previousSegmentsPositions = new Vector3[_segmentsCount];
			for(int i = 0; i < _segmentsCount - 1; i++)
			{
				var segment = Instantiate(_segmentPrefab, transform);
				var segmentPosition = segment.transform.position;
				segment.transform.position = new Vector3(segmentPosition.x, segmentPosition.y, (i + 1) / 100f);
				_segments[i] = segment;
			}
			_segments[_segmentsCount - 1] = Instantiate(_assPrefab, transform);
			transform.parent = null;
		}
		private void Update()
		{
			if(_target == null)
			{
				Destroy(gameObject);
				return;
			}
			var position = _target.position;
			var targetToSegment = (_segmentsPositions[0] - position).normalized;
			_segmentsPositions[0] = Vector3.SmoothDamp(_segmentsPositions[0],
				position + targetToSegment * _targetDistance,
				ref _segmentVelocities[0], _smoothSpeed);
			for(var i = 1; i < _segmentsCount; i++)
			{
				var targetToSegment2 = (_segmentsPositions[i] - _segmentsPositions[i - 1]).normalized;
				_segmentsPositions[i] = Vector3.SmoothDamp(_segmentsPositions[i],
					_segmentsPositions[i - 1] + targetToSegment2 * _targetDistance,
					ref _segmentVelocities[i], _smoothSpeed);
			}
			_segments[0].transform.position = new Vector3(_segmentsPositions[0].x, _segmentsPositions[0].y, 1 / 100f);
			var firstDir = _segmentsPositions[0] - _target.position;
			var angle1 = Mathf.Atan2(firstDir.y, firstDir.x) * Mathf.Rad2Deg;
			_segments[0].transform.rotation = Quaternion.AngleAxis(angle1 - 90, Vector3.forward);
			for(var i = 1; i < _segmentsCount; i++)
			{
				//rotate towards movement direction
				var direction = _segmentsPositions[i - 1] - _segmentsPositions[i];
				var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				_segments[i].transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
				_previousSegmentsPositions[i] = _segmentsPositions[i];
				_segments[i].transform.position = new Vector3(_segmentsPositions[i].x, _segmentsPositions[i].y, (i + 1) / 100f);
			}
		}
	}
}