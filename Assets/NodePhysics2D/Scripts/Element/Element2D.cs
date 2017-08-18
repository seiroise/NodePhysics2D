using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// 要素
	/// </summary>
	public class Element2D {

		/// <summary>
		/// シミュレータ
		/// </summary>
		protected PhysicsSim _sim;

		public Element2D(PhysicsSim sim) {
			_sim = sim;
		}
	}
}