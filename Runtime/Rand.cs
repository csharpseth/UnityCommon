using UnityEngine;

namespace MooshieGames.Common
{
	public static class Rand
	{
		public static Vector3 InsideRegion(Vector3 center, float xWidth, float zLength)
		{
			var temp = center;
			temp.x = center.x + Random.Range(-xWidth / 2, xWidth / 2);
			temp.z = center.z + Random.Range(-zLength / 2, zLength / 2);
			return temp;
		}
	}
}