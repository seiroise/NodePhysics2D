using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodePhysics2D.Core;

namespace NodePhysics2D.Demo {

	/// <summary>
	/// 三角形構造オブジェクトのデモ
	/// </summary>
	public class TriangleObject : MonoBehaviour {

		[SerializeField]
		private PhysicsSim _sim;

		[SerializeField, Range(1, 100)]
		private int _step = 3;

		[SerializeField, Range(0.01f, 20f)]
		private float _power = 1f;

		private Node2D _root;
		private Vector2 _left, _right;

		// Use this for initialization
		void Start() {
			_sim.Initialize();

			_left = RotateVector2(Vector2.up, 150f * Mathf.Deg2Rad);
			_right = RotateVector2(Vector2.up, -150f * Mathf.Deg2Rad);

			_root = _sim.MakeNode(transform.position);
			_root.locked = true;
			Node2D[] nodes = { _root };
			for (int i = 0; i < _step; ++i) {
				nodes = MakeStep(i + 1, nodes);
			}
		}

		// Update is called once per frame
		void Update() {
			if (Input.GetMouseButton(0)) {
				Vector3 mPos = Input.mousePosition;
				mPos.z = -Camera.main.transform.position.z;
				transform.position = Camera.main.ScreenToWorldPoint(mPos);
				// _root.SetForcePoint(transform.position);
			}
			_sim.Update(Time.deltaTime);
			transform.position = _sim.GetCenter();
		}

		private void OnDrawGizmos() {
			if (_sim != null) {
				_sim.DebugDraw();
			}
		}

		private Node2D[] MakeStep(int step, Node2D[] prevStepNodes) {
			Node2D[] nodes = new Node2D[step + 1];
			nodes[0] = _sim.MakeNode(prevStepNodes[0].point + _left);
			for (int i = 0; i < step; ++i) {
				nodes[i + 1] = _sim.MakeNode(prevStepNodes[i].point + _right);
				Link2D link;
				link = _sim.MakeLink(nodes[i], prevStepNodes[i]);
				link.constraint = new SpringLink2D(_sim, link, _power);

				link = _sim.MakeLink(prevStepNodes[i], nodes[i + 1]);
				link.constraint = new SpringLink2D(_sim, link, _power);

				link = _sim.MakeLink(nodes[i + 1], nodes[i]);
				link.constraint = new SpringLink2D(_sim, link, _power);
			}
			return nodes;
		}

		private Vector2 RotateVector2(Vector2 a, float angle) {
			float cos = Mathf.Cos(angle);
			float sin = Mathf.Sin(angle);
			return new Vector2(
				cos * a.x - sin * a.y,
				sin * a.x + cos * a.y
			);
		}
	}
}