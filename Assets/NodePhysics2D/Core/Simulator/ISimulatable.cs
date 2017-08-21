using UnityEngine;
using System.Collections;

namespace NodePhysics2D.Core {

	/// <summary>
	/// シミュレート可能
	/// </summary>
	public interface ISimulatable {

		/// <summary>
		/// ISimulatable
		/// シミュレータを返す
		/// </summary>
		/// <returns>The sim.</returns>
		PhysicsSim GetSim();
	}
}