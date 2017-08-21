using UnityEngine;
using System.Collections;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// ビューの要素
	/// </summary>
	[RequireComponent(typeof(Collider))]
	public abstract class ViewElement : MonoBehaviour {

		/// <summary>
		/// 管理しているビュー
		/// </summary>
		private LabView _view;

		/// <summary>
		/// 管理しているビューを設定する
		/// </summary>
		/// <param name="view">View.</param>
		public void SetView(LabView view) {
			_view = view;
		}

		/// <summary>
		/// 表示
		/// </summary>
		public abstract void Show();

		/// <summary>
		/// 非表示
		/// </summary>
		public abstract void Hide();

		/// <summary>
		/// 通常状態
		/// </summary>
		public abstract void ToNormal();

		/// <summary>
		/// 選択状態
		/// </summary>
		public abstract void ToSelect();
	}
}