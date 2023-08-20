using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
	public class GameManager : MonoBehaviour
	{
		private Dictionary<Type, object> _services = new Dictionary<Type, object>();
		public static GameManager Instance{get; private set;}
		private void Awake()
		{
			if(Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}
		private void OnDestroy()
		{
			if(Instance == this)
			{
				Instance = null;
			}
		}
		public void RegisterService<T>(T service, bool overrideExisting = false)
		{
			var type = typeof(T);
			if(_services.ContainsKey(type) && !overrideExisting)
			{
				Debug.LogError($"Service of type {type} already registered");
				return;
			}
			_services[type] = service;
		}
		public T GetService<T>()
		{
			var type = typeof(T);
			if(_services.ContainsKey(type)) return (T)_services[type];
			Debug.LogError($"Service of type {type} not registered");
			return default;
		}
	}
}