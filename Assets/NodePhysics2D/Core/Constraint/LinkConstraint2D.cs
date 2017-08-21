using UnityEngine;
using System.Collections;

namespace NodePhysics2D.Core {

	/// <summary>
	/// リンク制約
	/// </summary>
	public abstract class LinkConstraint2D : Constraint2D {

		/// <summary>
		/// 制約を行うリンク
		/// </summary>
		protected Link2D _link;

		public LinkConstraint2D(PhysicsSim sim, Link2D link) : base(sim) {
			_link = link;
		}
	}
}