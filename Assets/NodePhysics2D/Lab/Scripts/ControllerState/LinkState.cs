using UnityEngine;
using System.Collections;
using System;

namespace NodePhysics2D.Lab {

	public class LinkState : ControllerState {

		/// <summary>
		/// 選択している要素
		/// </summary>
		private ViewNode _nodeA;

		public LinkState(LabController controller) : base(controller) { }

		/// <summary>
		/// 状態に侵入
		/// </summary>
		public override void Enter() {
			_controller.view.ToNormalAllNodes();
			_controller.view.onNodeClicked.AddListener(OnElementClicked);
		}

		/// <summary>
		/// 状態から離脱
		/// </summary>
		public override void Exit() {
			_nodeA = null;
			_controller.view.ToNormalAllNodes();
			_controller.view.onNodeClicked.RemoveListener(OnElementClicked);
		}

		/// <summary>
		/// 状態の更新
		/// </summary>
		public override void Update() {
		}

		/// <summary>
		/// ノードのクリックコールバック
		/// </summary>
		/// <param name="node">Element.</param>
		private void OnElementClicked(ViewNode node) {
			if (_nodeA == null) {
				_nodeA = node;
				node.ToSelect();
			} else if (_nodeA == node) {
				return;
			} else {
				// リンクの作成
				if (_controller.model.MakeLink(_nodeA, node) != null) {
					_controller.view.ToNormalAllNodes();
					_nodeA = null;
				}
			}
		}
	}
}