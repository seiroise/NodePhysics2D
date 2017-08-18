using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// シミュレータの設定
	/// </summary>
	[System.Serializable]
	[CreateAssetMenu(fileName = "SimSetting", menuName = "NodePhysics2D/SimSetting")]
	public class SimSettings : ScriptableObject {

		[Header("Parameters")]

		/// <summary>
		/// バネ定数
		/// </summary>
		[SerializeField, Range(0.01f, 2f)]
		private float _springConstant = 0.9f;

		public float springConstant {
			get {
				return _springConstant;
			}
		}

		/// <summary>
		/// 角度定数
		/// </summary>
		[SerializeField, Range(0.01f, 2f)]
		private float _angleConstant = 0.2f;

		public float angleConstant {
			get {
				return _angleConstant;
			}
		}

		/// <summary>
		/// 減衰を使用するか
		/// </summary>
		[SerializeField]
		private bool _useDamping = true;

		public bool useDamping {
			get {
				return _useDamping;
			}
		}

		/// <summary>
		/// 減衰率
		/// </summary>
		[SerializeField, Range(0.8f, 0.99f)]
		private float _dampingRatio = 0.9f;

		public float dampingRatio {
			get {
				return _dampingRatio;
			}
		}

		/// <summary>
		/// 重力加速度
		/// </summary>
		[SerializeField]
		private Vector2 _gravity = new Vector2(0f, 0f);

		public Vector2 gravity {
			get {
				return _gravity;
			}
		}

		/// <summary>
		/// 重力加速度のスケール
		/// </summary>
		[SerializeField, Range(0f, 2f)]
		private float _gravityScale = 1f;

		public float gravityScale {
			get {
				return _gravityScale;
			}
		}

		[Header("Debug Parameter")]

		/// <summary>
		/// デバッグ描画時のノードの色
		/// </summary>
		[SerializeField]
		private Color _nodeColor = Color.green;

		public Color nodeColor {
			get {
				return _nodeColor;
			}
		}

		/// <summary>
		/// デバッグ描画時のノードの大きさ
		/// </summary>
		[SerializeField, Range(0.01f, 1f)]
		private float _nodeScale = 0.1f;

		public float nodeScale {
			get {
				return _nodeScale;
			}
		}

		/// <summary>
		/// ノードに掛かる合力の表示上の大きさ
		/// </summary>
		[SerializeField, Range(0.01f, 20f)]
		private float _resultantScale = 1f;

		public float resultantScale {
			get {
				return _resultantScale;
			}
		}

		/// <summary>
		/// デバッグ描画時のリンクの色
		/// </summary>
		[SerializeField]
		private Color _linkColor = Color.yellow;

		public Color linkColor {
			get {
				return _linkColor;
			}
		}

		/// <summary>
		/// デバッグ描画時のヒンジの色
		/// </summary>
		[SerializeField]
		private Color _hingeColor = Color.cyan;

		public Color hingeColor {
			get {
				return _hingeColor;
			}
		}
	}
}