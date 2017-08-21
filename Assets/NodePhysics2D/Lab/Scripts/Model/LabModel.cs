﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodePhysics2D.Core;
using UnityEngine.EventSystems;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// モデル
	/// </summary>
	[RequireComponent(typeof(LabView))]
	public class LabModel : MonoBehaviour {

		/// <summary>
		/// シミュレータ
		/// </summary>
		[SerializeField]
		private PhysicsSim _sim;

		public PhysicsSim sim {
			get {
				return _sim;
			}
		}

		/// <summary>
		/// シミュレータを再生するか
		/// </summary>
		private bool _isPlaySim;

		public bool isPlaySim {
			get {
				return _isPlaySim;
			}
			set {
				_isPlaySim = value;
			}
		}

		/// <summary>
		/// ビュー
		/// </summary>
		private LabView _view;

		/// <summary>
		/// リンクを構成するノードの辞書
		/// </summary>
		private Dictionary<ViewNode, List<ViewNode>> _linkDic;

		private void Awake() {
			_sim = new PhysicsSim();
			_sim.Initialize();

			_view = GetComponent<LabView>();

			_linkDic = new Dictionary<ViewNode, List<ViewNode>>();
		}

		private void Update() {
			if (_isPlaySim) {
				_sim.Update(Time.deltaTime);
			}
		}

		private void OnDrawGizmos() {
			if (_isPlaySim) {
				//_sim.DebugDraw();
			}
			_sim.DebugDraw();
		}

		/// <summary>
		/// ノードを作成する
		/// </summary>
		/// <param name="point">Point.</param>
		public ViewNode MakeNode(Vector2 point) {
			var node = _sim.MakeNode(point);
			var vNode = _view.MakeNode(point);
			vNode.node2D = node;
			return vNode;
		}

		/// <summary>
		/// リンクを作成する
		/// </summary>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public ViewLink MakeLink(ViewNode a, ViewNode b) {
			if (IsContainsLink(a, b)) return null;
			var link = _sim.MakeLink(a.node2D, b.node2D);
			var vLink = _view.MakeLink(link);
			if (!_linkDic.ContainsKey(a)) {
				_linkDic.Add(a, new List<ViewNode>());
			}
			_linkDic[a].Add(b);
			return vLink;
		}

		/// <summary>
		/// 指定したノードで構成されたリンクが含まれるか確認する
		/// </summary>
		/// <returns><c>true</c>, if contains link was ised, <c>false</c> otherwise.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public bool IsContainsLink(ViewNode a, ViewNode b) {
			if (_linkDic.ContainsKey(a)) {
				if (_linkDic[a].Contains(b)) {
					return true;
				}
			} else if (_linkDic.ContainsKey(b)) {
				if (_linkDic[b].Contains(a)) {
					return true;
				}
			}
			return false;
		}
	}
}