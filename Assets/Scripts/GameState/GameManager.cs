using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameState
{
	public class GameManager
	{
		private Dictionary<Type, object> _services = new Dictionary<Type, object>();
		public static GameManager Instance{get; private set;}
		public GameManager()
		{
			if(Instance != null)
			{
				Debug.LogError("Multiple instances of GameManager");
				return;
			}
			Instance = this;
		}
		public void RegisterService<T>(T service, bool overrideExisting = false) //should be a static method
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
		public void Dispose()
		{
			Instance = null;
		}
	}
}