using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// リンクに関する制約
	/// </summary>
	public abstract class LinkConstraint2D : Constraint2D {

		/// <summary>
		/// 対象の二つのノード
		/// </summary>
		protected Node2D _a, _b;

		/// <summary>
		/// 二つのノードの距離
		/// </summary>
		/// <value>The length of the current.</value>
		public float currentLength {
			get {
				return Node2D.Distance(_a, _b);
			}
		}

		/// <summary>
		/// 二つのノードの距離の二乗
		/// </summary>
		/// <value>The length of the sqr current.</value>
		public float sqrCurrentLength {
			get {
				return Node2D.SqrDistance(_a, _b);
			}
		}

		public LinkConstraint2D(PhysicsSim sim, Node2D a, Node2D b) : base(sim) {
			_a = a;
			_b = b;
		}
	}
}