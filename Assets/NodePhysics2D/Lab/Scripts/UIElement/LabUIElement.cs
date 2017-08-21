using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace NodePhysics2D.Lab {

	[System.Serializable]
	public class UIElementEvent : UnityEvent { }

	/// <summary>
	/// ラボのUI要素
	/// </summary>
	public abstract class LabUIElement : MonoBehaviour {

		/// <summary>
		/// 管理しているUI
		/// </summary>
		private LabUI _ui;

		/// <summary>
		/// 表示しているか
		/// </summary>
		private bool _isShow;

		private UIElementEvent _onShow;

		public UIElementEvent onShow {
			get {
				return _onShow;
			}
		}

		private UIElementEvent _onHide;

		public UIElementEvent onHide {
			get {
				return _onHide;
			}
		}

		protected virtual void Awake() {
			_onShow = new UIElementEvent();
			_onHide = new UIElementEvent();
		}

		public void Initialize(LabUI ui) {
			_ui = ui;
		}

		#region Show

		/// <summary>
		/// 表示
		/// </summary>
		public void Show() {
			_isShow = true;
			ShowMethod();
			if (_onShow != null) {
				_onShow.Invoke();
			}
		}

		/// <summary>
		/// 強制表示
		/// </summary>
		public void ShowEnforced() {
			_isShow = true;
			ShowEnforcedMethod();
			if (_onShow != null) {
				_onShow.Invoke();
			}
		}

		/// <summary>
		/// 表示方法
		/// </summary>
		protected abstract void ShowMethod();

		/// <summary>
		/// 強制表示方法
		/// </summary>
		protected abstract void ShowEnforcedMethod();

		#endregion

		#region Hide

		/// <summary>
		/// 非表示
		/// </summary>
		public void Hide() {
			_isShow = false;
			HideMethod();
			if (_onHide != null) {
				_onHide.Invoke();
			}
		}

		/// <summary>
		/// 強制非表示
		/// </summary>
		public void HideEnforced() {
			_isShow = false;
			HideEnforcedMethod();
			if (_onHide != null) {
				_onHide.Invoke();
			}
		}

		/// <summary>
		/// 非表示方法
		/// </summary>
		protected abstract void HideMethod();

		/// <summary>
		/// 強制非表示方法
		/// </summary>
		protected abstract void HideEnforcedMethod();

		#endregion
	}
}