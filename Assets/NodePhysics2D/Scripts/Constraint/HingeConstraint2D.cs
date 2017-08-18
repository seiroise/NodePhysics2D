using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// ヒンジ制約
	/// </summary>
	[System.Serializable]
	public abstract class HingeConstraint2D : Constraint2D {

		/// <summary>
		/// 制約を行うヒンジ
		/// </summary>
		protected Hinge2D _hinge;

		public HingeConstraint2D(PhysicsSim sim, Hinge2D hinge) : base(sim) {
			_hinge = hinge;
		}
	}
}