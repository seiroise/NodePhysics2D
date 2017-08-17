using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// デフォルトの物理演算器
	/// </summary>
	public class DefaultPhysicsProcessor : PhysicsProcessor {

		/// <summary>
		/// 前回のステップからの更新間隔
		/// </summary>
		private float _delta;

		public DefaultPhysicsProcessor(PhysicsSim sim) : base(sim) { }

		/// <summary>
		/// 更新処理
		/// </summary>
		/// <param name="delta">Delta.</param>
		public override void Update(float delta) {
			_delta = delta;
			UpdateGravity();
			if (sim.settings.updateSpring) {
				UpdateSpring();
			}
			if (sim.settings.updateString) {
				UpdateString();
			}
			if (sim.settings.useDamping) {
				UpdateDamping();
			}
		}

		/// <summary>
		/// 重力による座標の更新
		/// </summary>
		private void UpdateGravity() {
			Vector2 g = sim.settings.gravity * sim.settings.gravityScale * _delta;
			Node2D n;
			for (int i = 0; i < sim.NumberOfNodes(); ++i) {
				n = sim.GetNode(i);
				if (n.isFree) {
					n.point += g;
				}
			}
		}

		/// <summary>
		/// バネ制約の更新
		/// </summary>
		private void UpdateSpring() {
			Spring2D s;
			for (int i = 0; i < sim.NumberOfSprings(); ++i) {
				s = sim.GetSpring(i);
				if (s.enable) {
					s.Update(_delta);
				}
			}
		}

		/// <summary>
		/// ヒモ制約の更新
		/// </summary>
		private void UpdateString() {
			String2D s;
			for (int i = 0; i < sim.NumberOfStrings(); ++i) {
				s = sim.GetString(i);
				if (s.enable) {
					s.Update(_delta);
				}
			}
		}

		/// <summary>
		/// 減衰による座標の更新
		/// </summary>
		private void UpdateDamping() {
			Node2D n;
			Vector2 temp;
			float damping = sim.settings.dampingRatio;
			for (int i = 0; i < sim.NumberOfNodes(); ++i) {
				n = sim.GetNode(i);
				if (n.isFree) {
					temp = n.point;
					n.point += (n.point - n.prevPoint) * damping;
					n.prevPoint = temp;
				}
			}
		}
	}
}