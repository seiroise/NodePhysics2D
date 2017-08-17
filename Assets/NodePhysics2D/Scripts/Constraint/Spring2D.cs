﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodePhysics2D {

	/// <summary>
	/// 二つのノード間に働くバネ制約
	/// </summary>
	[System.Serializable]
	public class Spring2D : LinkConstraint2D, IDebugDrawable {

		/// <summary>
		/// 制約の強さ
		/// </summary>
		private float _power;

		/// <summary>
		/// 固定距離の二乗
		/// </summary>
		private float _constantlength2;

		public Spring2D(PhysicsSim sim, Node2D a, Node2D b, float power, float length) : base(sim, a, b) {
			_constantlength2 = length * length;
			_power = power;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public override void Update(float deltaTime) {
			if (_enable && (_a.isFree || _b.isFree)) {
				// 二つのノードの距離を保つ
				Vector2 delta = _a.point - _b.point;
				float t = (_constantlength2 / (delta.sqrMagnitude + _constantlength2) - 0.5f) * deltaTime * _power * _sim.settings.springConstant;
				if (_a.isFree) {
					_a.point += delta * t;
				}
				if (_b.isFree) {
					_b.point -= delta * t;
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