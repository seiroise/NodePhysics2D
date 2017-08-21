using UnityEngine;
using System.Collections;

namespace NodePhysics2D.Core {

	/// <summary>
	/// 物理演算機
	/// </summary>
	public abstract class PhysicsProcessor {

		/// <summary>
		/// シミュレータ
		/// </summary>
		private PhysicsSim _sim;

		public PhysicsSim sim {
			get {
				return _sim;
			}
		}

		public PhysicsProcessor(PhysicsSim sim) {
			_sim = sim;
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		/// <param name="delta">Delta.</param>
		public abstract void Update(float delta);
	}
}