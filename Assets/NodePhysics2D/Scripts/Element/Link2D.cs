using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// ノード間をつなぐリンク
	/// </summary>
	[System.Serializable]
	public class Link2D {

		/// <summary>
		/// リンクを行うノード
		/// </summary>
		protected Node2D _a, _b;

		/// <summary>
		/// リンクの長さ
		/// </summary>
		/// <value>The length of the current.</value>
		public float length {
			get {
				return Node2D.Distance(_a, _b);
			}
		}

		/// <summary>
		/// リンクの長さの二乗
		/// </summary>
		/// <value>The length of the sqr.</value>
		public float sqrLength {
			get {
				return Node2D.SqrDistance(_a, _b);
			}
		}

	}
}