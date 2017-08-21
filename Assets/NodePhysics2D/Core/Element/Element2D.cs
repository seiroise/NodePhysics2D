using UnityEngine;
using System.Collections;

namespace NodePhysics2D.Core {

	/// <summary>
	/// 要素
	/// </summary>
	public class Element2D {

		/// <summary>
		/// シミュレータ
		/// </summary>
		protected PhysicsSim _sim;

		public PhysicsSim sim {
			get {
				return _sim;
			}
		}

		public Element2D(PhysicsSim sim) {
			_sim = sim;
		}
	}
}