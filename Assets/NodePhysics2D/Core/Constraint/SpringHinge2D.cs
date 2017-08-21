using UnityEngine;
using System.Collections;
using System;

namespace NodePhysics2D.Core {

	/// <summary>
	/// ヒンジに働くバネ制約
	/// </summary>
	public class SpringHinge2D : HingeConstraint2D {

		/// <summary>
		/// 制約の強さ
		/// </summary>
		private float _power;

		/// <summary>
		/// 固定角度
		/// </summary>
		private float _constantRadian;

		public SpringHinge2D(PhysicsSim sim, Hinge2D hinge, float power) : base(sim, hinge) {
			_power = power;
			_constantRadian = hinge.DeltaRadian();
		}

		/// <summary>
		/// 更新
		/// </summary>
		public override void Update() {
			if (_enable && (_hinge.a.isFree || _hinge.b.isFree)) {
				// ヒンジのなす角度を保つ
				float delta = _constantRadian - _hinge.DeltaRadian();
				delta *= _power * _sim.settings.angleConstant;
				if (_hinge.a.isFree) {
					Vector2 t = RotateDelta(_hinge.a.point, _hinge.m.point, -delta);
					_hinge.a.AddResultant(t.x, t.y);
				}
				if (_hinge.b.isFree) {
					Vector2 t = RotateDelta(_hinge.b.point, _hinge.m.point, delta);
					_hinge.b.AddResultant(t.x, t.y);
				}
			}
		}

		/// <summary>
		/// ベクトルaをベクトルmを軸にangleだけ回転させた差分を返す
		/// </summary>
		/// <returns>The delta.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="m">M.</param>
		/// <param name="angle">Angle.</param>
		private Vector2 RotateDelta(Vector2 a, Vector2 m, float angle) {
			Vector2 v = a - m;
			return Vec2Util.Rotate(v, angle) - v;
		}
	}
}