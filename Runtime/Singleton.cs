using UnityEngine;

namespace MooshieGames.Common
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		[Header("Attributes")]
		[Tooltip("Keeps the singleton active through scene changes.")] [SerializeField]
		protected bool dontDestroyOnLoad = true;

		private static T _instance;

		protected static T Instance
		{
			get
			{
				if (Application.isPlaying == false)
				{
					Debug.LogError($"Attempt to access instance in edit mode for type: {typeof(T).Name}");
					Cleanup();
					return null;
				}

				if (_instance.OrNull() == null) _instance = FindFirstObjectByType<T>();

				return _instance;
			}
		}

		protected virtual void Awake()
		{
			if (_instance == null)
			{
				_instance = this as T;
				if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
				return;
			}

			Destroy(gameObject);
		}

		private static void Cleanup() => _instance = null;
	}
}