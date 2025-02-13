using System.IO;
using UnityEngine;

namespace MooshieGames.Common
{
	public static class JSONLoader
	{
		public static bool TryLoad<T>(out T result, string path)
		{
			if (File.Exists(path) == false)
			{
				result = default;
				return false;
			}

			try
			{
				var content = File.ReadAllText(path);
				result = JsonUtility.FromJson<T>(content);
				return true;
			}
			catch
			{
				result = default;
				return false;
			}
		}

		public static void Save(string value, string path) => File.WriteAllText(path, value);
	}
}