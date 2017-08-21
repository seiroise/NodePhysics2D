using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using NodePhysics2D.Core;

namespace NodePhysics2D.Lab {

	[System.Serializable]
	public class NodeEvent : UnityEvent<ViewNode> { }

	[System.Serializable]
	public class LinkEvent : UnityEvent<ViewLink> { }

	/// <summary>
	/// ラボのビュー管理
	/// </summary>
	public class LabView : MonoBehaviour {

		/// <summary>
		/// ノードのプレハブ
		/// </summary>
		[SerializeField]
		private ViewNode _viewNodePrefab;

		/// <summary>
		/// 管理しているノード
		/// </summary>
		private List<ViewNode> _nodes;

		/// <summary>
		/// リンクプレハブ
		/// </summary>
		[SerializeField]
		private ViewLink _viewLinkPrefab;

		/// <summary>
		/// 管理しているリンク要素
		/// </summary>
		private List<ViewLink> _links;

		/// <summary>
		/// ノードのクリック
		/// </summary>
		private NodeEvent _onNodeClicked;

		public NodeEvent onNodeClicked {
			get {
				return _onNodeClicked;
			}
		}

		/// <summary>
		/// リンクのクリック
		/// </summary>
		private LinkEvent _onLinkClicked;

		public LinkEvent onLinkClicked {
			get {
				return _onLinkClicked;
			}
		}

		private void Awake() {
			_nodes = new List<ViewNode>();
			_links = new List<ViewLink>();
			_onNodeClicked = new NodeEvent();
			_onLinkClicked = new LinkEvent();
		}

		#region Element

		/// <summary>
		/// 管理下のノードのクリック
		/// </summary>
		/// <param name="node">Node.</param>
		public void OnNodeClicked(ViewNode node) {
			_onNodeClicked.Invoke(node);
		}

		/// <summary>
		/// 管理下のリンクのクリック
		/// </summary>
		/// <param name="link">Link.</param>
		public void OnLinkClicked(ViewLink link) {
			_onLinkClicked.Invoke(link);
		}

		#endregion

		#region Node

		/// <summary>
		/// ノードを生成する
		/// </summary>
		private ViewNode InstantiateMonoNode2D() {
			var node = Instantiate<ViewNode>(_viewNodePrefab);
			node.name = "ViewNode";
			node.transform.SetParent(transform, true);
			return node;
		}

		/// <summary>
		/// リンクを作成する
		/// </summary>
		/// <param name="point">Point.</param>
		public ViewNode MakeNode(Vector2 point) {
			var node = InstantiateMonoNode2D();
			node.ToNormal();
			node.SetView(this);
			node.onCLicked.AddListener(OnNodeClicked);
			node.transform.position = point;
			_nodes.Add(node);
			return node;
		}

		/// <summary>
		/// すべてのノードを通常状態にする
		/// </summary>
		public void ToNormalAllNodes() {
			for (int i = 0; i < _nodes.Count; ++i) {
				_nodes[i].ToNormal();
			}
		}

		#endregion

		#region Link

		/// <summary>
		/// リンク要素を生成する
		/// </summary>
		/// <returns>The view link.</returns>
		private ViewLink InstantiateViewLink() {
			var link = Instantiate<ViewLink>(_viewLinkPrefab);
			link.name = "ViewLink";
			link.transform.SetParent(transform, true);
			return link;
		}

		/// <summary>
		/// リンクを作成する
		/// </summary>
		/// <returns>The link.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public ViewLink MakeLink(Link2D link) {
			var vLink = InstantiateViewLink();
			vLink.SetLink(link);
			vLink.ToNormal();
			vLink.SetView(this);
			vLink.onClicked.AddListener(OnLinkClicked);
			_links.Add(vLink);
			return vLink;
		}

		/// <summary>
		/// すべてのリンクを通常状態にする
		/// </summary>
		public void ToNormalAllLinks() {
			for (int i = 0; i < _links.Count; ++i) {
				_links[i].ToNormal();
			}
		}

		#endregion
	}
}