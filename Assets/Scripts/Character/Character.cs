using System;
using System.Collections.Generic;
using Characters.Inventories;
using Characters.Items;
using Characters.Items.Implementations.Flare;
using Characters.Movement;
using UnityEngine;

namespace Characters
{
	public class Character : MonoBehaviour, ICharacter, IDamagable, IAttacker
	{
		public event Action<int> OnHealthChanged;

		[SerializeField] private Rigidbody2D _rigidbody2D;
		[SerializeField] private float _speed;
		[SerializeField] private int _maxHealth;
		[SerializeField] private FlareItem _flareItem; //TODO: remove this

		private IInventory _inventory;
		private IItem _currentItem;
		private Vector2 _lookAtSpot;


		private int _health;

		private float _timeSinceLastAttack;
		protected virtual void Awake()
		{
			_health = _maxHealth;
		}
		protected virtual void Start()
		{
			//TODO: remove this
			_inventory = new Inventory(3);
			_inventory.AddItem(_flareItem);
			_currentItem = _inventory.GetItem(0);
			_timeSinceLastAttack = GetAttackSpeed();
		}
		public void Move(Vector2 direction)
		{
			direction = direction.normalized;
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

		//todo: move this to a separate class
		public virtual void Attack()
		{
			_timeSinceLastAttack += Time.deltaTime;
			if(!(_timeSinceLastAttack >= GetAttackSpeed())) return;
			_timeSinceLastAttack = 0f;

			var results = new Collider2D[10];
			var size = Physics2D.OverlapCircleNonAlloc(_rigidbody2D.position, GetAttackRange(), results);

			for(var i = 0; i < size; i++)
			{
				var damagable = results[i].GetComponent<IDamagable>();
				if(damagable != null && damagable.GetTeamId() != GetTeamId())
					damagable.TakeDamage(GetDamage());
			}
		}


		public int GetHealth()
		{
			return _health;
		}

		public void TakeDamage(int damage)
		{
			_health -= damage;
			OnHealthChanged?.Invoke(_health);
			if(_health <= 0)
				Die();
		}

		public virtual void Die()
		{
			Destroy(gameObject);
		}

		public virtual int GetTeamId()
		{
			return 1;
		}

		public int GetDamage()
		{
			return 1;
		}

		public float GetAttackRange()
		{
			return 1f;
		}

		public float GetAttackSpeed()
		{
			return 1f;
		}
	}
}