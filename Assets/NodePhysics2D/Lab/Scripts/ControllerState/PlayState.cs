using UnityEngine;
using System.Collections;
using System;

namespace NodePhysics2D.Lab {

	public class PlayState : ControllerState {

		private bool _isPlay;

		public PlayState(LabController controller) : base(controller) { }

		public override void Enter() {
			_controller.ui.playBtn.onClick.AddListener(OnPlayBtnClicked);
			_controller.model.isPlaySim = true;
		}

		public override void Exit() {
			_controller.ui.playBtn.onClick.RemoveListener(OnPlayBtnClicked);
			_controller.model.isPlaySim = false;
		}

		public override void Update() {
		}

		/// <summary>
		/// 再生ボタンのクリックイベント
		/// </summary>
		private void OnPlayBtnClicked() {
			_controller.model.isPlaySim = !_controller.model.isPlaySim;
		}
	}
}