using System;
using System.Collections.Generic;
using System.Linq;
using MooshieGames.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MooshieGames.Common
{
	public class ServiceLocator : MonoBehaviour
	{
		private static ServiceLocator _global;
		private static Dictionary<Scene, ServiceLocator> _sceneContainers;
		private static List<GameObject> _tempSceneGameObjects;

		private readonly ServiceManager _services = new();

		private const string k_globalServiceLocatorName = "ServiceLocator [Global]";
		private const string k_sceneServiceLocatorName = "ServiceLocator [Scene]";

		internal void ConfigureAsGlobal(bool dontDestroyOnLoad)
		{
			if (_global == this)
			{
				Debug.LogWarning("ServiceLocator.ConfigureAsGlobal: Already configured as global.");
			}
			else if (_global != null)
			{
				Debug.LogError("ServiceLocator.ConfigureAsGlobal: Another ServiceLocator is already configured as global.");
			}
			else
			{
				_global = this;
				if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
			}
		}

		internal void ConfigureForScene()
		{
			var scene = gameObject.scene;

			if (_sceneContainers.ContainsKey(scene))
			{
				Debug.LogError("ServiceLocator.ConfigureForScene: Another ServiceLocator is already configured for this scene.");
				return;
			}

			_sceneContainers.Add(scene, this);
		}

		public static ServiceLocator Global
		{
			get
			{
				if (_global != null) return _global;

				if (FindFirstObjectByType<ServiceLocatorGlobalBootstrapper>() is { } found)
				{
					found.BootstrapOnDemand();
					return _global;
				}

				var container = new GameObject(k_globalServiceLocatorName, typeof(ServiceLocator));
				container.AddComponent<ServiceLocatorGlobalBootstrapper>().BootstrapOnDemand();

				return _global;
			}
		}

		public static ServiceLocator For(MonoBehaviour mb) => mb.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(mb) ?? Global;

		public static ServiceLocator ForSceneOf(MonoBehaviour mb)
		{
			var scene = mb.gameObject.scene;

			if (_sceneContainers.TryGetValue(scene, out var container) && container != mb) return container;

			_tempSceneGameObjects.Clear();
			scene.GetRootGameObjects(_tempSceneGameObjects);

			foreach (var go in _tempSceneGameObjects.Where(go => go.GetComponent<ServiceLocatorSceneBootstrapper>() != null))
				if (go.TryGetComponent<ServiceLocatorSceneBootstrapper>(out var bootstrapper) && bootstrapper.Container != mb)
				{
					bootstrapper.BootstrapOnDemand();
					return bootstrapper.Container;
				}

			return Global;
		}

		public ServiceLocator Register<T>(T service)
		{
			_services.Register(service);
			return this;
		}

		public ServiceLocator Register(Type type, object service)
		{
			_services.Register(type, service);
			return this;
		}

		public ServiceLocator Get<T>(out T service) where T : class
		{
			if (TryGetService(out service)) return this;

			if (TryGetNextInHierarchy(out var container))
			{
				container.Get(out service);
				return this;
			}

			throw new ArgumentException($"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered.");
		}

		private bool TryGetService<T>(out T service) where T : class => _services.TryGet(out service);

		private bool TryGetNextInHierarchy(out ServiceLocator container)
		{
			if (this == _global)
			{
				container = null;
				return false;
			}

			container = transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(this);
			return container != null;
		}

		private void OnDestroy()
		{
			if (this == _global) _global = null;
			else if (_sceneContainers.ContainsValue(this)) _sceneContainers.Remove(gameObject.scene);
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetStatics()
		{
			_global = null;
			_sceneContainers = new Dictionary<Scene, ServiceLocator>();
			_tempSceneGameObjects = new List<GameObject>();
		}

#if UNITY_EDITOR
		[MenuItem("GameObject/ServiceLocator/Add Global")]
		private static void AddGlobal()
		{
			var go = new GameObject(k_globalServiceLocatorName, typeof(ServiceLocatorGlobalBootstrapper));
		}

		[MenuItem("GameObject/ServiceLocator/Add Scene")]
		private static void AddScene()
		{
			var go = new GameObject(k_sceneServiceLocatorName, typeof(ServiceLocatorSceneBootstrapper));
		}
#endif
	}
}