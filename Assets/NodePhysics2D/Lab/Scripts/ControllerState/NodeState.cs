using UnityEngine;
using System.Collections;
using System;

namespace NodePhysics2D.Lab {

	public class NodeState : ControllerState {
		
		public NodeState(LabController controller) : base(controller) { }

		/// <summary>
		/// 状態に侵入
		/// </summary>
		public override void Enter() {
			
		}

		/// <summary>
		/// 状態から離脱
		/// </summary>
		public override void Exit() {
			
		}

		/// <summary>
		/// 状態の更新
		/// </summary>
		public override void Update() {
			if (_controller.ui.eventSystem.IsPointerOverGameObject()) return;
			if (Input.GetMouseButtonDown(0)) {
				Vector3 pos = Input.mousePosition;
				pos.z = -Camera.main.transform.position.z;
				pos = Camera.main.ScreenToWorldPoint(pos);
				_controller.model.MakeNode(pos);
			}
		}
	}
}