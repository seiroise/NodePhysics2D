using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace NodePhysics2D.Lab {

	[System.Serializable]
	public class FloatEvent : UnityEvent<float> { }

	/// <summary>
	/// SliderとInputFieldの合体
	/// </summary>
	public class UISliderField : MonoBehaviour {

		/// <summary>
		/// スライダー
		/// </summary>
		[SerializeField]
		private Slider _slider;

		/// <summary>
		/// 入力
		/// </summary>
		[SerializeField]
		private InputField _input;

		/// <summary>
		/// 値変更イベント
		/// </summary>
		private FloatEvent _onValueChanged;

		public FloatEvent onValueChanged {
			get {
				return _onValueChanged;
			}
		}

		private void Awake() {
			_slider.onValueChanged.AddListener(OnValueChanged);
			_input.onEndEdit.AddListener(OnEndEdit);

			_onValueChanged = new FloatEvent();
		}

		/// <summary>
		/// スライダーの値変更イベント
		/// </summary>
		/// <param name="value">Value.</param>
		private void OnValueChanged(float v) {
			_input.text = v.ToString();
			_onValueChanged.Invoke(v);
		}

		/// <summary>
		/// 入力フィールドの値決定イベント
		/// </summary>
		/// <param name="str">String.</param>
		private void OnEndEdit(string str) {
			float result;
			if (float.TryParse(str, out result)) {
				_slider.value = result;
				_onValueChanged.Invoke(result);
			}
		}

		/// <summary>
		/// 値を設定する
		/// </summary>
		/// <param name="v">V.</param>
		public void SetValue(float v) {
			_slider.value = v;
			_input.text = v.ToString();
		}
	}
}