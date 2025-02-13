using UnityEngine;

namespace MooshieGames.Common
{
	public static class TransformExtensions
	{
		public static Vector3[] ToPositions(this Transform[] locations)
		{
			var positions = new Vector3[locations.Length];
			for (var i = 0; i < locations.Length; i++)
			{
				positions[i] = locations[i].position;
			}

			return positions;
		}
	}
}