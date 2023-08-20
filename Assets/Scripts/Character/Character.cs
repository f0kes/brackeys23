using System;
using System.Collections.Generic;
using Character.Inventory;
using Character.Items;
using Character.Items.Implementations.Flare;
using Characters.Movement;
using UnityEngine;

namespace Characters
{
	public class Character : MonoBehaviour, ICharacter
	{
		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private float _speed;
		[SerializeField] private FlareItem _flareItem; //TODO: remove this

		private IInventory _inventory;
		private IItem _currentItem;
		private Vector2 _lookAtSpot;
		private void Start()
		{
			//TODO: remove this
			_inventory = new Inventory(3);
			_inventory.AddItem(_flareItem);
			_currentItem = _inventory.GetItem(0);
		}
		public void Move(Vector2 direction)
		{
			_rigidbody2D.velocity = direction * _speed;
		}

		public Vector2 GetPosition()
		{
			return _rigidbody2D.position;
		}

		public void LookAt(Vector2 spot)
		{
			var position = _rigidbody2D.position;
			_lookAtSpot = spot;
			var direction = spot - position;
			var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			_rigidbody2D.rotation = angle;
		}

		public Vector2 GetLookSpot()
		{
			return _lookAtSpot;
		}

		public void ChangeItem(int itemIndex)
		{
			_currentItem = _inventory.GetItem(itemIndex);
		}

		public void UseItem()
		{
			_currentItem?.Use(this);
		}
	}
}