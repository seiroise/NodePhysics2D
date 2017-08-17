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
		/// バネ制約を更新するか
		/// </summary>
		[SerializeField]
		private bool _updateSpring = true;

		public bool updateSpring {
			get {
				return _updateSpring;
			}
		}

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
		/// ヒモ制約を更新するか
		/// </summary>
		[SerializeField]
		private bool _updateString = true;

		public bool updateString {
			get {
				return _updateString;
			}
		}

		/// <summary>
		/// 角度制約を更新するか
		/// </summary>
		[SerializeField]
		private bool _updateAngle = true;

		public bool upsateAngle {
			get {
				return _updateAngle;
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
		[SerializeField, Range(0f, 10f)]
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
		/// デバッグ描画時のバネの色
		/// </summary>
		[SerializeField]
		private Color _springColor = Color.yellow;

		public Color springColor {
			get {
				return _springColor;
			}
		}

		/// <summary>
		/// デバッグ描画時のヒモの色
		/// </summary>
		[SerializeField]
		private Color _stringColor = Color.cyan;

		public Color stringColor {
			get {
				return _stringColor;
			}
		}
	}
}