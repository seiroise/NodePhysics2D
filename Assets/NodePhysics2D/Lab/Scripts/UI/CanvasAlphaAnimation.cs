using UnityEngine;
using System.Collections;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// CanvasGroupのAlphaのアニメーション
	/// </summary>
	[RequireComponent(typeof(CanvasGroup))]
	public class CanvasAlphaAnimation : MonoBehaviour {

		private CanvasGroup _canvas;

		/// <summary>
		/// 変化時間
		/// </summary>
		[SerializeField]
		private float _time = 1f;

		/// <summary>
		/// 計測時間
		/// </summary>
		private float _mTime;

		/// <summary>
		/// 透明度の変化曲線
		/// </summary>
		[SerializeField]
		private AnimationCurve _alphaCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		/// <summary>
		/// 再生中か
		/// </summary>
		private bool _play;

		/// <summary>
		/// 再生方向が順方向か
		/// </summary>
		private bool _forward;

		private void Awake() {
			_canvas = GetComponent<CanvasGroup>();
		}

		private void Update() {
			if (_play) {
				if (_forward) {
					_mTime += Time.deltaTime;
					if (_mTime < _time) {
						_canvas.alpha = _alphaCurve.Evaluate(_mTime / _time);
					} else {
						ShowEnforced();
					}
				} else {
					_mTime -= Time.deltaTime;
					if (_mTime > 0f) {
						_canvas.alpha = _alphaCurve.Evaluate(_mTime / _time);
					} else {
						HideEnforced();
					}
				}
			}
		}

		/// <summary>
		/// 表示する
		/// </summary>
		public void Show() {
			_play = true;
			_forward = true;
			_canvas.blocksRaycasts = true;
		}

		/// <summary>
		/// 強制的に表示する
		/// </summary>
		public void ShowEnforced() {
			_play = false;
			_mTime = _time;
			_canvas.alpha = 1f;
			_canvas.blocksRaycasts = true;
		}

		/// <summary>
		/// 非表示にする
		/// </summary>
		public void Hide() {
			_play = true;
			_forward = false;
			_canvas.blocksRaycasts = false;
		}

		/// <summary>
		/// 強制的に非表示にする
		/// </summary>
		public void HideEnforced() {
			_play = false;
			_mTime = 0f;
			_canvas.alpha = 0f;
			_canvas.blocksRaycasts = false;
		}
	}
}