using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class CharacterMover : MonoBehaviour, IMover
{
	[SerializeField] private Rigidbody2D _rigidbody2D;
	[SerializeField] private float _speed;

	public void Move(Vector2 direction)
	{
		_rigidbody2D.velocity = direction * _speed;
	}

	public void LookAt(Vector2 spot)
	{
		var position = _rigidbody2D.position;
		var direction = spot - position;
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		_rigidbody2D.rotation = angle;
	}
}