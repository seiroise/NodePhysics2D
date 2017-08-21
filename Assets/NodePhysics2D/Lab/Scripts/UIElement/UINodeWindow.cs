using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using NodePhysics2D.Core;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// ノードの情報を表示するウィンドウ
	/// </summary>
	public class UINodeWindow : UIWindow {

		/// <summary>
		/// 関連付けしているノード
		/// </summary>
		private Node2D _node;

		[Header("Node UI Parts")]

		/// <summary>
		/// ロック要素のフィールド
		/// </summary>
		[SerializeField]
		private Toggle _lockField;

		public Toggle lockField {
			get {
				return _lockField;
			}
		}

		/// <summary>
		/// ダンピング要素のフィールド
		/// </summary>
		[SerializeField]
		private UISliderField _dampingField;

		public UISliderField dampingField {
			get {
				return _dampingField;
			}
		}

		/// <summary>
		/// 情報を表示するノードを設定する
		/// </summary>
		/// <param name="node">Node.</param>
		public void SetNode(Node2D node) {
			_node = node;
			_lockField.isOn = _node.locked;
			_dampingField.SetValue(node.damping);
		}
	}
}