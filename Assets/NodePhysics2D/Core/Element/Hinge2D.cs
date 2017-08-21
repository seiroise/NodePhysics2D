using UnityEngine;
using System.Collections;
using System;

namespace NodePhysics2D.Core {

	/// <summary>
	/// 3つのノードから作られるヒンジ
	/// ノードは必ず反時計周りに配置される
	/// </summary>
	[System.Serializable]
	public class Hinge2D : Element2D, IDebugDrawable {

		/// <summary>
		/// ヒンジを構成するノード
		/// </summary>
		private Node2D _a, _b, _m;

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

		public Node2D m {
			get {
				return _m;
			}
		}

		/// <summary>
		/// ヒンジに掛ける制約
		/// </summary>
		private HingeConstraint2D _constraint;

		public HingeConstraint2D constraint {
			get {
				return _constraint;
			}
			set {
				_constraint = value;
			}
		}

		/// <summary>
		/// maベクトル
		/// </summary>
		/// <value>The ma.</value>
		public Vector2 ma {
			get {
				return _a.point - _m.point;
			}
		}

		/// <summary>
		/// mbベクトル
		/// </summary>
		/// <value>The mb.</value>
		public Vector2 mb {
			get {
				return _m.point - _b.point;
			}
		}

		public Hinge2D(PhysicsSim sim, Node2D a, Node2D b, Node2D m) : base(sim) {
			if (Vec2Util.CCW(a.point, m.point, b.point)) {
				_a = a;
				_b = b;
			} else {
				_a = b;
				_b = a;
			}
			_m = m;
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
		/// ab,bcベクトルのなす角を求める
		/// </summary>
		/// <returns>The radian.</returns>
		public float DeltaRadian() {
			float a1 = Vec2Util.Radian(ma);
			float a2 = Vec2Util.Radian(mb);
			float d = a2 - a1;
			if (d > Mathf.PI) {
				d -= Mathf.PI * 2f;
			} else if (d < -Mathf.PI) {
				d += Mathf.PI * 2f;
			}
			return d;
		}

		/// <summary>
		/// IDebugDrawable
		/// デバッグ用の描画
		/// </summary>
		public void DebugDraw() {
			Vector2 mid = ((_a.point + _b.point) - (_m.point * 2)) * 0.25f;
			Gizmos.DrawLine(_m.point, _m.point + mid);
		}
	}
}