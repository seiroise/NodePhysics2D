﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodePhysics2D.Core;

namespace NodePhysics2D.Demo {

	/// <summary>
	/// バネオブジェクトのデモ
	/// </summary>
	public class SpringObject : MonoBehaviour, ISimulatable {

		[SerializeField]
		private PhysicsSim _sim;

		[SerializeField, Range(2, 50)]
		private int _numberOfNodes = 10;

		[SerializeField, Range(0.01f, 10f)]
		private float _interval = 1f;

		[SerializeField, Range(0.01f, 100f)]
		private float _power = 1f;

		private Node2D _root;

		// Use this for initialization
		void Start() {
			_sim.Initialize();

			Node2D node = _root = _sim.MakeNode(transform.position);
			_root.locked = true;
			for (int i = 1; i < _numberOfNodes; ++i) {
				Node2D temp = _sim.MakeNode(node.point - new Vector2(0f, _interval));
				Link2D link = _sim.MakeLink(node, temp);
				link.constraint = new SpringLink2D(_sim, link, 1f, _interval);
				node = temp;
			}
		}

		// Update is called once per frame
		void Update() {
			if (Input.GetMouseButtonDown(0)) {
				Vector3 mPos = Input.mousePosition;
				mPos.z = -Camera.main.transform.position.z;
				transform.position = Camera.main.ScreenToWorldPoint(mPos);
			}
			// _root.point = transform.position;
			_sim.Update(Time.deltaTime);
		}

		private void OnDrawGizmos() {
			if (_sim != null) {
				_sim.DebugDraw();
			}
		}

		/// <summary>
		/// ISimulatable
		/// シミュレータを返す
		/// </summary>
		/// <returns>The sim.</returns>
		public PhysicsSim GetSim() {
			return _sim;
		}
	}
}