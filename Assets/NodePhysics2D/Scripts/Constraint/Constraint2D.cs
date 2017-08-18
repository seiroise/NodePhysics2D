using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// 制約
	/// </summary>
	[System.Serializable]
	public abstract class Constraint2D {

		/// <summary>
		/// この力を働かせるか
		/// </summary>
		protected bool _enable = true;

		public bool enable {
			get {
				return _enable;
			}
			set {
				_enable = value;
			}
		}

		/// <summary>
		/// シミュレータ
		/// </summary>
		[System.NonSerialized]
		protected PhysicsSim _sim;

		public PhysicsSim sim {
			get {
				return _sim;
			}
		}

		public Constraint2D(PhysicsSim sim) {
			_sim = sim;
		}

		/// <summary>
		/// 更新
		/// </summary>
		public abstract void Update();
	}

}