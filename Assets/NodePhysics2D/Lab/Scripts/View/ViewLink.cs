using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using NodePhysics2D.Core;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// ビューのリンク要素
	/// </summary>
	[RequireComponent(typeof(LineRenderer), typeof(MeshCollider))]
	public class ViewLink : ViewElement {

		/// <summary>
		/// レンダラ
		/// </summary>
		private LineRenderer _renderer;

		/// <summary>
		/// コライダ
		/// </summary>
		private MeshCollider _collider;

		/// <summary>
		/// クリック時コールバック
		/// </summary>
		private LinkEvent _onClicked;

		public LinkEvent onClicked {
			get {
				return _onClicked;
			}
		}

		/// <summary>
		/// 対応するリンク
		/// </summary>
		private Link2D _link2D;

		public Link2D link2D {
			get {
				return _link2D;
			}
		}

		/// <summary>
		/// 当たり判定用のメッシュ
		/// </summary>
		private Mesh _mesh;

		private void Awake() {
			_renderer = GetComponent<LineRenderer>();
			_collider = GetComponent<MeshCollider>();
			_onClicked = new LinkEvent();
		}

		private void OnMouseUp() {
			_onClicked.Invoke(this);
		}

		public override void Hide() { }

		public override void Show() { }

		public override void ToNormal() {
			_renderer.startColor = Color.black;
			_renderer.endColor = Color.black;
		}

		public override void ToSelect() {
			_renderer.startColor = Color.red;
			_renderer.endColor = Color.red;
		}

		/// <summary>
		/// リンクを設定する
		/// </summary>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public void SetLink(Link2D link) {
			_link2D = link;
			_renderer.positionCount = 2;
			_renderer.SetPosition(0, link.a.point);
			_renderer.SetPosition(1, link.b.point);

			if (_mesh == null) {
				_mesh = new Mesh();
				_mesh.name = "Link";
				_mesh.MarkDynamic();
				_mesh.vertices = GetVertices();
				_mesh.SetIndices(GetIndices(), MeshTopology.Triangles, 0);
			} else {
				_mesh.vertices = GetVertices();
			}
			_mesh.RecalculateBounds();

			_collider.sharedMesh = _mesh;
		}

		/// <summary>
		/// リンクを構成する頂点を取得する
		/// </summary>
		/// <returns>The vertices.</returns>
		private Vector3[] GetVertices() {
			Vector3[] vertices = new Vector3[4];
			float width = _renderer.startWidth * 0.5f;
			Vector2 v = (_link2D.abVert / _link2D.length) * width;
			vertices[0] = _link2D.a.point + v;
			vertices[1] = _link2D.a.point - v;
			vertices[2] = _link2D.b.point - v;
			vertices[3] = _link2D.b.point + v;
			return vertices;
		}

		/// <summary>
		/// リンクを構成するインデックスを取得する
		/// </summary>
		/// <returns>The indices.</returns>
		private int[] GetIndices() {
			int[] indices = new int[6];
			indices[0] = 0;
			indices[1] = 2;
			indices[2] = 1;

			indices[3] = 0;
			indices[4] = 3;
			indices[5] = 2;

			return indices;
		}
	}
}