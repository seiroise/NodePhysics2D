using UnityEngine;
using System.Collections;
using System;
using NodePhysics2D.Core;

namespace NodePhysics2D.Lab {


	public class SelectState : ControllerState {

		/// <summary>
		/// 選択中のノード
		/// </summary>
		private ViewNode _curNode;

		/// <summary>
		/// 選択中のリンク
		/// </summary>
		private ViewLink _curLink;

		private UINodeWindow _nodeWindow;

		private UILinkWindow _linkWindow;

		private readonly string _uiGroupName = "Window";

		private readonly string _nodeWindowName = "NodeWindow";

		private readonly string _linkWindowName = "LinkWindow";

		public SelectState(LabController controller) : base(controller) { }

		public override void Enter() {
			_controller.view.onNodeClicked.AddListener(OnNodeClicked);
			_controller.view.onLinkClicked.AddListener(OnLinkClicked);

			_nodeWindow = (UINodeWindow)_controller.ui.GetUIElement(_uiGroupName, _nodeWindowName);
			_linkWindow = (UILinkWindow)_controller.ui.GetUIElement(_uiGroupName, _linkWindowName);

			_nodeWindow.lockField.onValueChanged.AddListener(OnNodeLockChanged);
			_nodeWindow.dampingField.onValueChanged.AddListener(OnNodeDampingChanged);

			_linkWindow.constraintField.onValueChanged.AddListener(OnLinkConstraintChanged);
			_linkWindow.powerField.onValueChanged.AddListener(OnLinkPowerChanged);
			_linkWindow.restLengthField.onValueChanged.AddListener(OnLinkRestlengthChanged);
		}

		public override void Exit() {
			_controller.view.onNodeClicked.RemoveListener(OnNodeClicked);
			_controller.view.onLinkClicked.RemoveListener(OnLinkClicked);

			_nodeWindow.lockField.onValueChanged.RemoveListener(OnNodeLockChanged);
			_nodeWindow.dampingField.onValueChanged.RemoveListener(OnNodeDampingChanged);

			_linkWindow.constraintField.onValueChanged.RemoveListener(OnLinkConstraintChanged);
			_linkWindow.powerField.onValueChanged.RemoveListener(OnLinkPowerChanged);
			_linkWindow.restLengthField.onValueChanged.RemoveListener(OnLinkRestlengthChanged);

			_controller.ui.HideUIElement("Window");
		}

		public override void Update() { }

		/// <summary>
		/// ノードのクリックコールバック
		/// </summary>
		/// <param name="node">Node.</param>
		private void OnNodeClicked(ViewNode node) {
			if (_controller.ui.eventSystem.IsPointerOverGameObject()) return;
			var w = (UINodeWindow)_controller.ui.ShowUIElement(_uiGroupName, _nodeWindowName);
			if (w) {
				_curNode = node;
				w.SetNode(node.node2D);
			}
		}

		/// <summary>
		/// リンクのクリックコールバック
		/// </summary>
		/// <param name="link">Link.</param>
		private void OnLinkClicked(ViewLink link) {
			if (_controller.ui.eventSystem.IsPointerOverGameObject()) return;
			var w = (UILinkWindow)_controller.ui.ShowUIElement("Window", "LinkWindow");
			if (w) {
				w.SetLink(link.link2D);
				_curLink = link;
			}
		}

		#region NodeWindow

		/// <summary>
		/// ノードのLockの変更
		/// </summary>
		/// <param name="v">If set to <c>true</c> v.</param>
		private void OnNodeLockChanged(bool v) {
			if (_curNode) {
				_curNode.node2D.locked = v;
			}
		}

		/// <summary>
		/// ノードのDampingの変更
		/// </summary>
		/// <param name="v">V.</param>
		private void OnNodeDampingChanged(float v) {
			if (_curNode) {
				_curNode.node2D.damping = v;
			}
		}

		#endregion

		#region LinkWindow

		/// <summary>
		/// リンクのConstraintの変更
		/// </summary>
		/// <param name="i">The index.</param>
		private void OnLinkConstraintChanged(int i) {
			if (!_curLink) return;
			string opt = _linkWindow.constraintField.options[i].text;
			switch (opt) {
			case "None":
				_curLink.link2D.constraint = null;
				break;
			case "Spring":
				var spring = new SpringLink2D(_controller.model.sim, _curLink.link2D, 1f);
				_curLink.link2D.constraint = spring;
				_linkWindow.powerField.SetValue(spring.power);
				_linkWindow.restLengthField.SetValue(Mathf.Sqrt(spring.restlength2));
				break;
			}
		}

		/// <summary>
		/// リンクのバネ制約のpowerの変更
		/// </summary>
		/// <param name="v">V.</param>
		private void OnLinkPowerChanged(float v) {
			if (!_curLink) return;
			var constraint = _curLink.link2D.constraint;
			if (constraint is SpringLink2D) {
				var spring = (SpringLink2D)constraint;
				spring.power = v;
			}
		}

		/// <summary>
		/// リンクのバネ制約のrestlengthの変更
		/// </summary>
		/// <param name="v">V.</param>
		private void OnLinkRestlengthChanged(float v) {
			if (!_curLink) return;
			var constraint = _curLink.link2D.constraint;
			if (constraint is SpringLink2D) {
				var spring = (SpringLink2D)constraint;
				spring.restlength2 = v * v;
			}
		}

		#endregion
	}
}