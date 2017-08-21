using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NodePhysics2D.Core {

	/// <summary>
	/// 物理演算を行うシミュレータ
	/// </summary>
	[System.Serializable]
	public class PhysicsSim {

		/// <summary>
		/// デフォルトの設定を上書きする場合
		/// </summary>
		[SerializeField]
		private bool _overrideSetting;

		/// <summary>
		/// ユーザの設定
		/// </summary>
		[SerializeField]
		private SimSettings _userSettings;

		/// <summary>
		/// 適用されている設定
		/// </summary>
		private SimSettings _settings;

		public SimSettings settings {
			get {
				return _settings;
			}
		}

		/// <summary>
		/// シミュレータの管理クラス
		/// </summary>
		private SimManager _manager;

		/// <summary>
		/// このシミュレータの処理するノード
		/// </summary>
		[System.NonSerialized]
		private List<Node2D> _nodes;

		/// <summary>
		/// このシミュレータの処理するリンク
		/// </summary>
		[System.NonSerialized]
		private List<Link2D> _links;

		/// <summary>
		/// このシミュレータの処理するヒンジ
		/// </summary>
		[System.NonSerialized]
		private List<Hinge2D> _hinges;

		/// <summary>
		/// 物理演算機
		/// </summary>
		private PhysicsProcessor _processer;

		/// <summary>
		/// 初期化処理。起動時に必ず呼び出すこと
		/// </summary>
		public void Initialize() {
			_manager = SimManager.instance;
			if (_overrideSetting && _userSettings) {
				_settings = _userSettings;
			} else {
				_settings = _manager.settings;
			}
			_nodes = new List<Node2D>();
			_links = new List<Link2D>();
			_hinges = new List<Hinge2D>();

			_processer = new DefaultPhysicsProcessor(this);
		}

		/// <summary>
		/// 更新処理
		/// </summary>
		/// <param name="delta">Delta time.</param>
		public void Update(float delta) {
			_processer.Update(delta);
		}

		#region Node

		/// <summary>
		/// 指定した座標にノードを作成する
		/// </summary>
		/// <returns>The node.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public Node2D MakeNode(float x, float y) {
			var n = new Node2D(this, new Vector2(x, y), _settings.dampingRatio);
			_nodes.Add(n);
			return n;
		}

		/// <summary>
		/// 指定した座標にノードを作成する
		/// </summary>
		/// <returns>The node.</returns>
		/// <param name="point">Point.</param>
		public Node2D MakeNode(Vector2 point) {
			var n = new Node2D(this, point, _settings.dampingRatio);
			_nodes.Add(n);
			return n;
		}

		/// <summary>
		/// ノードの数を返す
		/// </summary>
		/// <returns>The of nodes.</returns>
		public int NumberOfNodes() {
			return _nodes == null ? 0 : _nodes.Count;
		}

		/// <summary>
		/// 指定したインデックスのノードを返す
		/// </summary>
		/// <returns>The node.</returns>
		/// <param name="idx">Index.</param>
		public Node2D GetNode(int idx) {
			return _nodes[idx];
		}

		/// <summary>
		/// ノードの中心座標を返す
		/// </summary>
		/// <returns>The center.</returns>
		public Vector2 GetCenter() {
			Vector2 c = Vector2.zero;
			int count = NumberOfNodes();
			for (int i = 0; i < count; ++i) {
				c.x += _nodes[i].point.x;
				c.y += _nodes[i].point.y;
			}
			c.x /= count;
			c.y /= count;
			return c;
		}

		#endregion

		#region Link

		/// <summary>
		/// 指定したノードでリンクを作成する
		/// </summary>
		/// <returns>The link.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public Link2D MakeLink(Node2D a, Node2D b) {
			var l = new Link2D(this, a, b);
			_links.Add(l);
			return l;
		}

		/// <summary>
		/// リンクの数を返す
		/// </summary>
		/// <returns>The of links.</returns>
		public int NumberOfLinks() {
			return _links == null ? 0 : _links.Count;
		}

		/// <summary>
		/// 指定したインデックスのリンクを返す
		/// </summary>
		/// <returns>The link.</returns>
		/// <param name="idx">Index.</param>
		public Link2D GetLink(int idx) {
			return _links[idx];
		}

		#endregion

		#region Hinge

		/// <summary>
		/// 指定したノードでヒンジを作成する
		/// </summary>
		/// <returns>The hinge.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="m">M.</param>
		public Hinge2D MakeHinge(Node2D a, Node2D b, Node2D m) {
			var h = new Hinge2D(this, a, b, m);
			_hinges.Add(h);
			return h;
		}

		/// <summary>
		/// ヒンジの数を返す
		/// </summary>
		/// <returns>The of hinges.</returns>
		public int NumberOfHinges() {
			return _hinges == null ? 0 : _hinges.Count;
		}

		/// <summary>
		/// 指定したインデックスのヒンジを返す
		/// </summary>
		/// <returns>The hinge.</returns>
		/// <param name="idx">Index.</param>
		public Hinge2D GetHinge(int idx) {
			return _hinges[idx];
		}

		#endregion

		#region Mesh

		/// <summary>
		/// メッシュを構成する頂点配列を返す
		/// </summary>
		/// <returns>The vertices.</returns>
		public Vector3[] GetVertices(float width) {
			Vector3[] vertices = new Vector3[NumberOfLinks() * 4];
			Vector3 a, b, h, v;
			Link2D l;
			for (int i = 0, j = 0; i < NumberOfLinks(); ++i, j += 4) {
				l = _links[i];
				a = l.a.point;
				b = l.b.point;
				h = l.mid;
				v = l.abVert / _links[i].length * width;

				vertices[j] 	= a;
				vertices[j + 3] = b;
				vertices[j + 1] = h + v;
				vertices[j + 2] = h - v;

			}
			return vertices;
		}

		/// <summary>
		/// メッシュを構成するインデックス配列を返す
		/// </summary>
		/// <returns>The indices.</returns>
		public int[] GetIndices() {
			int[] indices = new int[NumberOfLinks() * 6];
			for (int i = 0, j = 0, k = 0; i < NumberOfLinks(); ++i, j += 6, k += 4) {
				indices[j] 		= k;
				indices[j + 1] 	= k + 1;
				indices[j + 2] 	= k + 2;

				indices[j + 3] 	= k + 1;
				indices[j + 4] 	= k + 3;
				indices[j + 5] 	= k + 2;
			}
			return indices;
		}

		#endregion

		#region Debug

		/// <summary>
		/// すべての要素のデバッグ描画
		/// </summary>
		public void DebugDraw() {
			DebugDrawLink();
			DebugDrawHinge();
			DebugDrawNode();
		}

		/// <summary>
		/// ノードのデバッグ描画
		/// </summary>
		public void DebugDrawNode() {
			if (_settings == null) {
				return;
			}
			Gizmos.color = _settings.nodeColor;
			for (int i = 0; i < NumberOfNodes(); ++i) {
				_nodes[i].DebugDraw();
			}
		}

		/// <summary>
		/// リンクのデバッグ描画
		/// </summary>
		public void DebugDrawLink() {
			if (_settings == null) {
				return;
			}
			Gizmos.color = _settings.linkColor;
			for (int i = 0; i < NumberOfLinks(); ++i) {
				_links[i].DebugDraw();
			}
		}

		/// <summary>
		/// ヒンジのデバッグ描画
		/// </summary>
		public void DebugDrawHinge() {
			if (_settings == null) {
				return;
			}
			Gizmos.color = _settings.hingeColor;
			for (int i = 0; i < NumberOfHinges(); ++i) {
				_hinges[i].DebugDraw();
			}
		}

		#endregion
	}
}