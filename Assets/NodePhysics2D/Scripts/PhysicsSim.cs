using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NodePhysics2D {

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
		private List<Node2D> _nodes;

		/// <summary>
		/// このシミュレータで処理するバネ制約
		/// </summary>
		private List<Spring2D> _springs;

		/// <summary>
		/// このシミュレータで処理するヒモ制約
		/// </summary>
		private List<String2D> _strings;

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
			_springs = new List<Spring2D>();
			_strings = new List<String2D>();

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
			var n = new Node2D(new Vector2(x, y));
			_nodes.Add(n);
			return n;
		}

		/// <summary>
		/// 指定した座標にノードを作成する
		/// </summary>
		/// <returns>The node.</returns>
		/// <param name="point">Point.</param>
		public Node2D MakeNode(Vector2 point) {
			var n = new Node2D(point);
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

		#region Spring

		/// <summary>
		/// バネ制約を作成する
		/// </summary>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		/// <param name="length">Length.</param>
		public Spring2D MakeSpring(Node2D a, Node2D b, float power, float length) {
			var s = new Spring2D(this, a, b, power, length);
			_springs.Add(s);
			return s;
		}

		/// <summary>
		/// バネ制約を作成する。長さは自動的に計算する
		/// </summary>
		/// <returns>The spring.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public Spring2D MakeSpring(Node2D a, Node2D b, float power) {
			var s = new Spring2D(this, a, b, power, Node2D.Distance(a, b));
			_springs.Add(s);
			return s;
		}

		/// <summary>
		/// バネ制約の数を返す
		/// </summary>
		/// <returns>The of springs.</returns>
		public int NumberOfSprings() {
			return _springs == null ? 0 : _springs.Count;
		}

		/// <summary>
		/// 指定したインデックスのバネ制約を返す
		/// </summary>
		/// <returns>The spring.</returns>
		/// <param name="idx">Index.</param>
		public Spring2D GetSpring(int idx) {
			return _springs[idx];
		}

		#endregion

		#region String

		/// <summary>
		/// ヒモ制約を作成する
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public String2D MakeString(Node2D a, Node2D b) {
			var s = new String2D(this, a, b);
			_strings.Add(s);
			return s;
		}

		/// <summary>
		/// ヒモ制約の数を返す
		/// </summary>
		/// <returns>The of strings.</returns>
		public int NumberOfStrings() {
			return _strings == null ? 0 : _strings.Count;
		}

		/// <summary>
		/// 指定したインデックスのヒモ制約を返す
		/// </summary>
		/// <returns>The spring.</returns>
		/// <param name="idx">Index.</param>
		public String2D GetString(int idx) {
			return _strings[idx];
		}

		#endregion

		#region Debug

		/// <summary>
		/// すべての要素のデバッグ描画
		/// </summary>
		public void DebugDraw() {
			DebugDrawSpring();
			DebugDrawString();

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
		/// バネ制約のデバッグ描画
		/// </summary>
		public void DebugDrawSpring() {
			if (_settings == null) {
				return;
			}
			Gizmos.color = _settings.springColor;
			for (int i = 0; i < NumberOfSprings(); ++i) {
				_springs[i].DebugDraw();
			}
		}

		/// <summary>
		/// ヒモ制約のデバッグ描画
		/// </summary>
		public void DebugDrawString() {
			if (_settings == null) {
				return;
			}
			Gizmos.color = _settings.stringColor;
			for (int i = 0; i < NumberOfStrings(); ++i) {
				_strings[i].DebugDraw();
			}
		}

		#endregion
	}
}