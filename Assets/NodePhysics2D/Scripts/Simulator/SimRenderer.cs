using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// PhysicsSimの内容をメッシュとして描画
	/// </summary>
	public class SimRenderer : MonoBehaviour {

		/// <summary>
		/// マテリアル
		/// </summary>
		[SerializeField]
		private Material _mat;

		[SerializeField, Range(0.01f, 2f)]
		private float _width = 1f;

		/// <summary>
		/// シミュレータ
		/// </summary>
		private PhysicsSim _sim;

		/// <summary>
		/// メッシュ
		/// </summary>
		private Mesh _mesh;

		private void Start() {
			var simulatable = GetComponent<ISimulatable>();
			if (simulatable != null) {
				_sim = simulatable.GetSim();
				_mesh = new Mesh();
				_mesh.name = "SimMesh";
				//_mesh.MarkDynamic();
			}
		}

		private void Update() {
			if (_sim != null) {
				_mesh.vertices = _sim.GetVertices(_width);
				_mesh.SetIndices(_sim.GetIndices(), MeshTopology.Triangles, 0);
				_mesh.RecalculateBounds();
				Graphics.DrawMesh(_mesh, Matrix4x4.Translate(Vector3.zero), _mat, 0);
			}
		}
	}
}