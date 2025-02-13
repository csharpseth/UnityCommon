using System;
using System.Collections.Generic;
using UnityEngine;

namespace MooshieGames.Common
{
	public class ServiceManager
	{
		private readonly Dictionary<Type, object> _services = new();
		public IEnumerable<object> RegisteredServices => _services.Values;

		public bool TryGet<T>(out T service) where T : class
		{
			var type = typeof(T);
			if (_services.TryGetValue(type, out var obj))
			{
				service = obj as T;
				return true;
			}

			service = null;
			return false;
		}

		public T Get<T>() where T : class
		{
			var type = typeof(T);
			if (_services.TryGetValue(type, out var service)) return service as T;

			throw new ArgumentException($"ServiceManager.Get: Service of type {type} is not registered");
		}

		public ServiceManager Register<T>(T service)
		{
			var type = typeof(T);
			if (_services.TryAdd(type, service) == false) Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered.");

			return this;
		}

		public ServiceManager Register(Type type, object service)
		{
			if (type.IsInstanceOfType(service) == false) throw new ArgumentException("Type of service is not an instance of type: ", nameof(service));

			if (_services.TryAdd(type, service) == false) Debug.LogError($"ServiceManager.Register: Service of type {type.FullName} already registered.");

			return this;
		}
	}
}