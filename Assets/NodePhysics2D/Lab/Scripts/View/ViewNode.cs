using UnityEngine;
using System.Collections;
using System;
using NodePhysics2D.Core;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// ビューに表示するためのノード
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer), typeof(SphereCollider))]
	public class ViewNode : ViewElement {

		/// <summary>
		/// レンダラ
		/// </summary>
		private SpriteRenderer _renderer;

		/// <summary>
		/// クリック時コールバック
		/// </summary>
		private NodeEvent _onClicked;

		public NodeEvent onCLicked {
			get {
				return _onClicked;
			}
		}

		/// <summary>
		/// 対応するノード
		/// </summary>
		private Node2D _node2D;

		public Node2D node2D {
			get {
				return _node2D;
			}
			set {
				_node2D = value;
			}
		}

		private void Awake() {
			_renderer = GetComponent<SpriteRenderer>();
			_onClicked = new NodeEvent();
		}

		private void OnMouseUp() {
			_onClicked.Invoke(this);
		}

		public override void Hide() { }

		public override void Show() { }

		public override void ToNormal() {
			_renderer.color = Color.black;
		}

		public override void ToSelect() {
			_renderer.color = Color.red;
		}

	}
}