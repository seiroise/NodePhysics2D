using UnityEngine;
using System.Collections;
using System;

namespace NodePhysics2D.Core {

	/// <summary>
	/// ノード間をつなぐリンク
	/// </summary>
	[System.Serializable]
	public class Link2D : Element2D, IDebugDrawable {

		/// <summary>
		/// リンクを構成するノード
		/// </summary>
		private Node2D _a, _b;

		public Node2D a {
			get {
				return _a;
			}
		}

		public Node2D b {
			get {
				return _b;
			}
		}

		/// <summary>
		/// リンクに掛ける制約
		/// </summary>
		private LinkConstraint2D _constraint;

		public LinkConstraint2D constraint {
			get {
				return _constraint;
			}
			set {
				_constraint = value;
			}
		}

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

		/// <summary>
		/// aからbへのベクトル
		/// </summary>
		/// <value>The ab.</value>
		public Vector2 ab {
			get {
				return _b.point - _a.point;
			}
		}

		/// <summary>
		/// bからaへのベクトル
		/// </summary>
		/// <value>The ba.</value>
		public Vector2 ba {
			get {
				return _a.point - _b.point;
			}
		}

		/// <summary>
		/// 中点の座標ベクトルを返す
		/// </summary>
		/// <value>The middle.</value>
		public Vector2 mid {
			get {
				return (_a.point + _b.point) * 0.5f;
			}
		}

		/// <summary>
		/// aからbへのベクトルに垂直なベクトル
		/// </summary>
		/// <value>The vertical.</value>
		public Vector2 abVert {
			get {
				return Vec2Util.Rotate(ab, 90f * Mathf.Deg2Rad);
			}
		}

		public Link2D(PhysicsSim sim, Node2D a, Node2D b) : base(sim) {
			_a = a;
			_b = b;
		}

		/// <summary>
		/// 制約の更新
		/// </summary>
		public void Update() {
			if (_constraint != null) {
				_constraint.Update();
			}
		}

		/// <summary>
		/// IDebugDrawable
		/// デバッグ用の描画
		/// </summary>
		public void DebugDraw() {
			Gizmos.DrawLine(_a.point, _b.point);
		}
	}
}