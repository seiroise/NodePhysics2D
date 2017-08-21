using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// うぃんどう
	/// </summary>
	[RequireComponent(typeof(CanvasGroup), typeof(CanvasAlphaAnimation))]
	public class UIWindow : LabUIElement {

		[Header("UIParts")]

		/// <summary>
		/// 全体の背景
		/// </summary>
		[SerializeField]
		private Image _background;

		public Image background {
			get {
				return _background;
			}
		}

		/// <summary>
		/// ヘッダー
		/// </summary>
		[SerializeField]
		private Image _header;

		public Image header {
			get {
				return _header;
			}
		}

		/// <summary>
		/// ヘッダーのテキスト
		/// </summary>
		[SerializeField]
		private Text _headerTxt;

		public Text headerTxt {
			get {
				return _headerTxt;
			}
		}

		/// <summary>
		/// ヘッダーのボタン
		/// </summary>
		[SerializeField]
		private Button _headerBtn;

		public Button headerBtn {
			get {
				return _headerBtn;
			}
		}

		/// <summary>
		/// コンテナ領域
		/// </summary>
		[SerializeField]
		private RectTransform _container;

		public RectTransform container {
			get {
				return _container;
			}
		}

		/// <summary>
		/// 透過値のアニメーション
		/// </summary>
		private CanvasAlphaAnimation _alphaAnimation;

		protected override void Awake() {
			base.Awake();
			_alphaAnimation = GetComponent<CanvasAlphaAnimation>();
			_headerBtn.onClick.AddListener(Hide);
		}

		protected override void ShowMethod() {
			_alphaAnimation.Show();
		}

		protected override void HideMethod() {
			_alphaAnimation.Hide();
		}

		protected override void ShowEnforcedMethod() {
			_alphaAnimation.ShowEnforced();
		}

		protected override void HideEnforcedMethod() {
			_alphaAnimation.HideEnforced();
		}
	}
}