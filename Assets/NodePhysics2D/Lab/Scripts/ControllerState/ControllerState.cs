using UnityEngine;
using System.Collections;

namespace NodePhysics2D.Lab {

	public abstract class ControllerState {

		/// <summary>
		/// 状態を管理するコントローラ
		/// </summary>
		protected LabController _controller;

		public ControllerState(LabController controller) {
			_controller = controller;
		}

		/// <summary>
		/// 状態に侵入
		/// </summary>
		public abstract void Enter();

		/// <summary>
		/// 状態の更新
		/// </summary>
		public abstract void Update();

		/// <summary>
		/// 状態から離脱
		/// </summary>
		public abstract void Exit();
	}
}