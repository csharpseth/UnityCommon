using UnityEngine;

namespace MooshieGames.Common
{
	public static class Vector2Extensions
	{
		/// <summary>
		/// Sets any values of the Vector2
		/// </summary>
		public static Vector2 With(this Vector2 vector, float? x = null, float? y = null) => new(x ?? vector.x, y ?? vector.y);

		/// <summary>
		/// Adds to any values of the Vector3
		/// </summary>
		public static Vector2 Add(this Vector2 vector, float? x = null, float? y = null) => new(vector.x + (x ?? 0), vector.y + (y ?? 0));

		/// <summary>
		/// Converts a Vector2 to a Vector3 with a y value of 0.
		/// </summary>
		/// <param name="v2">The Vector2 to convert.</param>
		/// <returns>A Vector3 with the x and z values of the Vector2 and a y value of 0.</returns>
		public static Vector3 ToVector3(this Vector2 v2) => new(v2.x, 0, v2.y);
	}
}