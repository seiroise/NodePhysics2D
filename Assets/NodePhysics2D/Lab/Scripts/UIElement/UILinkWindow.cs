using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using NodePhysics2D.Core;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// リンクの情報を表示するウィンドウ
	/// </summary>
	public class UILinkWindow : UIWindow {

		/// <summary>
		/// 関連付けしているリンク
		/// </summary>
		private Link2D _link;

		[Header("Link UI Parts")]

		/// <summary>
		/// 制約のドロップダウン
		/// </summary>
		[SerializeField]
		private Dropdown _constraintField;

		public Dropdown constraintField {
			get {
				return _constraintField;
			}
		}

		/// <summary>
		/// バネ制約の力
		/// </summary>
		[SerializeField]
		private UISliderField _powerField;

		public UISliderField powerField {
			get {
				return _powerField;
			}
		}

		/// <summary>
		/// バネ制約の停止距離
		/// </summary>
		[SerializeField]
		private UISliderField _restlengthField;

		public UISliderField restLengthField {
			get {
				return _restlengthField;
			}
		}

		/// <summary>
		/// 情報を表示するリンクを設定する
		/// </summary>
		/// <param name="link">Link.</param>
		public void SetLink(Link2D link) {
			_link = link;
			if (link.constraint == null) {
				_constraintField.captionText.text = "None";
			} else if (link.constraint is SpringLink2D) {
				_constraintField.captionText.text = "Spring";
				var spring = (SpringLink2D)link.constraint;
				_powerField.SetValue(spring.power);
				_restlengthField.SetValue(Mathf.Sqrt(spring.restlength2));
			}
		}
	}
}