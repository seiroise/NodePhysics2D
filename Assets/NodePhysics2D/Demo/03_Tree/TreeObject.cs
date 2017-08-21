using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodePhysics2D.Core;

namespace NodePhysics2D.Demo {

	/// <summary>
	/// 木構造のオブジェクトのデモ
	/// </summary>
	public class TreeObject : MonoBehaviour {

		[SerializeField]
		private PhysicsSim _sim;

		[SerializeField, Range(0.01f, 10f)]
		private float _branchLength = 1f;

		[SerializeField, Range(1, 10)]
		private int _depth = 2;

		[SerializeField, Range(0.5f, 1f)]
		private float _damping = 0.8f;

		[SerializeField, Range(0.01f, 20f)]
		private float _power = 1f;

		private Node2D _root;

		private void Start() {
			_sim.Initialize();

			Vector3 pos = transform.position;

			_root = _sim.MakeNode(pos);
			_root.locked = true;
			Link2D link = _sim.MakeLink(_root, _sim.MakeNode(pos + new Vector3(0f, _branchLength)));

			MakeBranch(link, _depth);
		}

		private void Update() {
			_sim.Update(Time.deltaTime);
		}

		private void OnDrawGizmos() {
			_sim.DebugDraw();
		}

		private void MakeBranch(Link2D link, int depth) {
			link.constraint = new SpringLink2D(_sim, link, 1f);

			if (depth <= 0) return;

			Node2D l = _sim.MakeNode(Vec2Util.Rotate(link.ab, 30f * Mathf.Deg2Rad) * _damping + link.b.point);
			Node2D r = _sim.MakeNode(Vec2Util.Rotate(link.ab, -30f * Mathf.Deg2Rad) * _damping + link.b.point);

			Hinge2D h = _sim.MakeHinge(l, r, link.b);
			h.constraint = new SpringHinge2D(_sim, h, _power);
			Hinge2D hl = _sim.MakeHinge(link.a, l, link.b);
			hl.constraint = new SpringHinge2D(_sim, hl, _power);
			Hinge2D hr = _sim.MakeHinge(link.a, r, link.b);
			hl.constraint = new SpringHinge2D(_sim, hr, _power);

			MakeBranch(_sim.MakeLink(link.b, r), depth - 1);
			MakeBranch(_sim.MakeLink(link.b, l), depth - 1);
		}
	}
}