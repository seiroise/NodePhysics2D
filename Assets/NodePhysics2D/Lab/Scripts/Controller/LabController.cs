using UnityEngine;
using System.Collections;
using System;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// コントローラ
	/// </summary>
	[RequireComponent(typeof(LabView), typeof(LabModel), typeof(LabUI))]
	public class LabController : MonoBehaviour {

		/// <summary>
		/// 現在の状態
		/// </summary>
		private ControllerState _currentState;

		/// <summary>
		/// 現在の状態の種類
		/// </summary>
		private Type _currentStateType;

		/// <summary>
		/// ビュー
		/// </summary>
		private LabView _view;

		public LabView view {
			get {
				return _view;
			}
		}

		/// <summary>
		/// モデル
		/// </summary>
		private LabModel _model;

		public LabModel model {
			get {
				return _model;
			}
		}

		/// <summary>
		/// UI
		/// </summary>
		private LabUI _ui;

		public LabUI ui {
			get {
				return _ui;
			}
		}

		private void Awake() {
			_view = GetComponent<LabView>();
			_model = GetComponent<LabModel>();
			_ui = GetComponent<LabUI>();
		}

		private void Start() {
			_ui.nodeBtn.onClick.AddListener(OnNodeBtnClicked);
			_ui.selectBtn.onClick.AddListener(OnSelectBtnClicked);
			_ui.linkBtn.onClick.AddListener(OnLinkBtnClicked);
			_ui.playBtn.onClick.AddListener(OnPlayBtnClicked);
		}

		private void Update() {
			if (_currentState != null) {
				_currentState.Update();
			}
		}

		#region State

		/// <summary>
		/// 状態を変更する
		/// </summary>
		/// <param name="state">State.</param>
		private void ChangeState(ControllerState state) {
			Type t = state.GetType();
			if (_currentStateType == t) {
				return;
			}
			if (_currentState != null) {
				_currentState.Exit();
			}
			_currentStateType = t;
			_currentState = state;
			_currentState.Enter();
		}

		#endregion

		/// <summary>
		/// ノードボタンのクリックイベント
		/// </summary>
		private void OnNodeBtnClicked() {
			ChangeState(new NodeState(this));
		}

		/// <summary>
		/// リンクボタンのクリックイベント
		/// </summary>
		private void OnLinkBtnClicked() {
			ChangeState(new LinkState(this));
		}

		/// <summary>
		/// セレクトボタンのクリックイベント
		/// </summary>
		private void OnSelectBtnClicked() {
			ChangeState(new SelectState(this));
		}

		/// <summary>
		/// 再生ボタンのクリックイベント
		/// </summary>
		private void OnPlayBtnClicked() {
			ChangeState(new PlayState(this));
		}
	}
}