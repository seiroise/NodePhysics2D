using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// Vector2のよく使う処理
	/// </summary>
	public class Vec2Util {

		/// <summary>
		/// 指定したベクトルaを原点を軸にradianだけ回転させる
		/// </summary>
		/// <returns>The rotate.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="radian">Radian.</param>
		public static Vector2 Rotate(Vector2 a, float radian) {
			float cos = Mathf.Cos(radian);
			float sin = Mathf.Sin(radian);
			return new Vector2(
				cos * a.x - sin * a.y,
				sin * a.x + cos * a.y
			);
		}

		/// <summary>
		/// 指定したベクトルaを点pを軸にradianだけ回転させる
		/// </summary>
		/// <returns>The rotate.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="p">P.</param>
		/// <param name="radian">Radian.</param>
		public static Vector2 Rotate(Vector2 a, Vector2 p, float radian) {
			float cos = Mathf.Cos(radian);
			float sin = Mathf.Sin(radian);
			float dx = a.x - p.x;
			float dy = a.y - p.y;
			return new Vector2(
				cos * dx - sin * dy + p.x,
				sin * dx + cos * dy + p.y
			);
		}

		/// <summary>
		/// 指定したベクトルのradianを求める
		/// </summary>
		/// <returns>The angle.</returns>
		/// <param name="a">The alpha component.</param>
		public static float Radian(Vector2 a) {
			return Mathf.Atan2(a.y, a.x);
		}

		/// <summary>
		/// 指定したベクトルの外積を求める
		/// </summary>
		/// <returns>The cross.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static float Cross(Vector2 a, Vector2 b) {
			return a.x * b.y - b.x * a.y;
		}

		/// <summary>
		/// p1とp3がp2を軸に反時計周りで並んでいるか
		/// </summary>
		public static bool CCW(Vector2 p1, Vector2 p2, Vector2 p3) {
			return Cross(p2 - p1, p3 - p2) >= 0;
		}
	}
}