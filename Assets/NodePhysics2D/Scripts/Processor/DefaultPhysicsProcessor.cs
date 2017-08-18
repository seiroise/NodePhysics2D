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
			ApplyLinkConstraint();
			ApplyHingeConstraint();
			UpdateNode();
		}

		/// <summary>
		/// リンクの制約を適用
		/// </summary>
		private void ApplyLinkConstraint() {
			Link2D l;
			for (int i = 0; i < sim.NumberOfLinks(); ++i) {
				l = sim.GetLink(i);
				l.Update();
			}
		}

		/// <summary>
		/// ヒンジの制約のの適用
		/// </summary>
		private void ApplyHingeConstraint() {
			Hinge2D h;
			for (int i = 0; i < sim.NumberOfHinges(); ++i) {
				h = sim.GetHinge(i);
				h.Update();
			}
		}

		/// <summary>
		/// ノードの更新
		/// </summary>
		private void UpdateNode() {
			//重力
			Vector2 g = sim.settings.gravity * sim.settings.gravityScale;
			for (int i = 0; i < sim.NumberOfNodes(); ++i) {
				Node2D n = sim.GetNode(i);
				if (n.isFree) {
					// 重力の適応
					n.AddResultant(g.x, g.y);
					// 減衰の適応
					n.ApplyDamping();
					// 時間倍率の適用
					n.ScaleResultant(_delta);
					// 座標の更新
					n.ApplyResultant();
				}
			}
		}
	}
}