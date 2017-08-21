﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodePhysics2D.Core {

	/// <summary>
	/// リンクに働くバネ制約
	/// </summary>
	[System.Serializable]
	public class SpringLink2D : LinkConstraint2D {

		/// <summary>
		/// 制約の強さ
		/// </summary>
		private float _power;

		public float power {
			get {
				return _power;
			}
			set {
				_power = value;
			}
		}

		/// <summary>
		/// 静止距離の二乗
		/// </summary>
		private float _restlength2;

		public float restlength2 {
			get {
				return _restlength2;
			}
			set {
				_restlength2 = value;
			}
		}

		public SpringLink2D(PhysicsSim sim, Link2D link, float power) : base(sim, link) {
			_restlength2 = link.sqrLength;
			_power = power;
		}

		public SpringLink2D(PhysicsSim sim, Link2D link, float power, float length) : base(sim, link) {
			_restlength2 = length * length;
			_power = power;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public override void Update() {
			if (_enable && (_link.a.isFree || _link.b.isFree)) {
				// リンクの距離を保つ
				Vector2 delta = _link.ba;
				float t = (_restlength2 / (delta.sqrMagnitude + _restlength2) - 0.5f) * _power * _sim.settings.springConstant;
				if (_link.a.isFree) {
					Vector2 tv = delta * t;
					_link.a.AddResultant(tv.x, tv.y);
				}
				if (_link.b.isFree) {
					Vector2 tv = delta * -t;
					_link.b.AddResultant(tv.x, tv.y);
				}
			}
		}
	}
}