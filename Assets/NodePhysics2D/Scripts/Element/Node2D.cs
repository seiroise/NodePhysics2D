using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NodePhysics2D {

	/// <summary>
	/// 物理演算を行うノード
	/// </summary>
	[System.Serializable]
	public class Node2D : Element2D, IDebugDrawable {

		/// <summary>
		/// 現在の座標
		/// </summary>
		[SerializeField]
		private Vector2 _point;

		public Vector2 point {
			get {
				return _point;
			}
			/*
			set {
				_point = value;
			}
			*/
		}

		/// <summary>
		/// 座標を固定するか
		/// </summary>
		[SerializeField]
		private bool _locked;

		public bool locked {
			set {
				_locked = value;
			}
		}

		/// <summary>
		/// 座標が固定されているか
		/// </summary>
		/// <value><c>true</c> if is lock; otherwise, <c>false</c>.</value>
		public bool isLock {
			get {
				return _locked;
			}
		}

		/// <summary>
		/// 座標が固定されていないか
		/// </summary>
		/// <value><c>true</c> if is free; otherwise, <c>false</c>.</value>
		public bool isFree {
			get {
				return !_locked;
			}
		}

		/// <summary>
		/// 減衰
		/// </summary>
		private float _damping;

		public float damping {
			set {
				_damping = Mathf.Clamp(value, 0f, 0.99f);
			}
			get {
				return _damping;
			}
		}

		/// <summary>
		/// 合力
		/// </summary>
		private Vector2 _resultant;

		public Vector2 resultant {
			get {
				return _resultant;
			}
		}

		/// <summary>
		/// 一つ前の合力
		/// </summary>
		private Vector2 _prevResultant;

		public Vector2 prevResultant {
			get {
				return _prevResultant;
			}
		}

		public Node2D(PhysicsSim sim) : base(sim){
			_point = new Vector2(0f, 0f);
			_locked = false;
			_damping = 0f;
			ResetResultant();
		}

		public Node2D(PhysicsSim sim, Vector2 point, float damping) : base(sim){
			_point = point;
			_locked = false;
			this.damping = damping;
			ResetResultant();
		}

		/// <summary>
		/// 合力のリセット
		/// </summary>
		private void ResetResultant() {
			_prevResultant.x = _resultant.x;
			_prevResultant.y = _resultant.y;
			_resultant.x = 0f;
			_resultant.y = 0f;
		}

		/// <summary>
		/// 合力の追加
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public void AddResultant(float x, float y) {
			_resultant.x += x;
			_resultant.y += y;
		}

		/// <summary>
		/// 減衰を適用する
		/// </summary>
		public void ApplyDamping() {
			_resultant.x += _resultant.x * _damping;
			_resultant.y += _resultant.y * _damping;
		}

		/// <summary>
		/// 合力の乗算
		/// </summary>
		public void ScaleResultant(float scale) {
			_resultant.x *= scale;
			_resultant.y *= scale;
		}

		/// <summary>
		/// 合力を適用する
		/// </summary>
		public void ApplyResultant() {
			_point.x += _resultant.x;
			_point.y += _resultant.y;
			ResetResultant();
		}

		/// <summary>
		/// 二つのノード間の距離を返す
		/// </summary>
		/// <returns>The distance.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static float Distance(Node2D a, Node2D b) {
			float dx = a._point.x - b._point.x;
			float dy = a._point.y - b._point.y;
			return Mathf.Sqrt(dx * dx + dy * dy);
		}

		/// <summary>
		/// 二つのノード間の距離の二乗を返す
		/// </summary>
		/// <returns>The distance.</returns>
		/// <param name="a">The alpha component.</param>
		/// <param name="b">The blue component.</param>
		public static float SqrDistance(Node2D a, Node2D b) {
			float dx = a._point.x - b._point.x;
			float dy = a._point.y - b._point.y;
			return dx * dx + dy * dy;
		}

		/// <summary>
		/// IDebugDrawable
		/// デバッグ用の描画
		/// </summary>
		public void DebugDraw() {
			Gizmos.DrawSphere(_point, _sim.settings.nodeScale);
			Gizmos.DrawLine(_point, _point + _prevResultant * _sim.settings.resultantScale);
		}
	}
}