using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NodePhysics2D {

	/// <summary>
	/// 物理演算を行うノード
	/// </summary>
	[System.Serializable]
	public class Node2D : IDebugDrawable {

		/// <summary>
		/// 現在の座標
		/// </summary>
		[SerializeField]
		private Vector2 _point;

		public Vector2 point {
			get {
				return _point;
			}
			set {
				_point = value;
			}
		}

		/// <summary>
		/// 1ステップ前の座標
		/// </summary>
		private Vector2 _prevPoint;

		public Vector2 prevPoint {
			get {
				return _prevPoint;
			}
			set {
				_prevPoint = value;
			}
		}

		/// <summary>
		/// 座標を固定するか
		/// </summary>
		[SerializeField]
		private bool _locked;

		public bool locked {
			set {
				_locked = value;
			}
		}

		/// <summary>
		/// 座標が固定されているか
		/// </summary>
		/// <value><c>true</c> if is lock; otherwise, <c>false</c>.</value>
		public bool isLock {
			get {
				return _locked;
			}
		}

		/// <summary>
		/// 座標が固定されていないか
		/// </summary>
		/// <value><c>true</c> if is free; otherwise, <c>false</c>.</value>
		public bool isFree {
			get {
				return !_locked;
			}
		}

		public Node2D() {
			_point = _prevPoint = new Vector2(0f, 0f);
		}

		public Node2D(Vector2 point) {
			_point = _prevPoint = point;
		}

		/// <summary>
		/// 二つのノード間の距離を返す
		/// </summary>
		/// <returns>The distance.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static float Distance(Node2D a, Node2D b) {
			float dx = a._point.x - b._point.x;
			float dy = a._point.y - b._point.y;
			return Mathf.Sqrt(dx * dx + dy * dy);
		}

		/// <summary>
		/// 二つのノード間の距離の二乗を返す
		/// </summary>
		/// <returns>The distance.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static float SqrDistance(Node2D a, Node2D b) {
			float dx = a._point.x - b._point.x;
			float dy = a._point.y - b._point.y;
			return dx * dx + dy * dy;
		}

		/// <summary>
		/// IDebugDrawable
		/// デバッグ用の描画
		/// </summary>
		public void DebugDraw() {
			Gizmos.DrawSphere(_point, 0.1f);
		}
	}
}