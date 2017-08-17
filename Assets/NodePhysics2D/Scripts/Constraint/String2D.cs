using UnityEngine;
using System.Collections;
using System;

namespace NodePhysics2D {

	/// <summary>
	/// 二つのノード間に働くヒモ制約
	/// </summary>
	public class String2D : LinkConstraint2D, IDebugDrawable {

		/// <summary>
		/// 固定距離
		/// </summary>
		private float _constantLength;

		public String2D(PhysicsSim sim, Node2D a, Node2D b) : base(sim, a, b) {
			_constantLength = currentLength;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public override void Update(float deltaTime) {
			if (_enable && (_a.isFree || _b.isFree)) {
				// 二つのノード間の距離を保つ
				Vector2 dir = (_b.point - _a.point) / currentLength;
				if (_b.isFree) {
					
					_b.point = _a.point + (dir * _constantLength);
				}
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