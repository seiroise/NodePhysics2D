using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace NodePhysics2D.Lab {

	/// <summary>
	/// Unity組み込みのUIとのつなぎ
	/// </summary>
	public class LabUI : MonoBehaviour {

		/// <summary>
		/// ラボ内のUIElementの排他的なグループ
		/// このグループの中のUIElementは常に最大でひとつだけ表示される
		/// </summary>
		[System.Serializable]
		public class LabUIGroup {

			/// <summary>
			/// グループ名
			/// </summary>
			[SerializeField]
			private string _name;

			public string name {
				get {
					return _name;
				}
			}

			/// <summary>
			/// このグループに所属する要素
			/// </summary>
			[SerializeField]
			private LabUIElement[] _elements;

			/// <summary>
			/// 表示中の要素
			/// </summary>
			private LabUIElement _curElement;

			/// <summary>
			/// 要素の辞書
			/// </summary>
			private Dictionary<string, LabUIElement> _elementDic;

			/// <summary>
			/// 初期化。使用する場合は必ず呼び出す
			/// </summary>
			public void Initialize() {
				_elementDic = new Dictionary<string, LabUIElement>();
				for (int i = 0; i < _elements.Length; ++i) {
					_elementDic.Add(_elements[i].name, _elements[i]);
					if (!_elements[i].gameObject.activeInHierarchy) {
						_elements[i].gameObject.SetActive(true);
					}
					_elements[i].HideEnforced();
					_elements[i].onHide.AddListener(OnHide);
				}
			}

			/// <summary>
			/// 指定した名前のUI要素を表示する
			/// </summary>
			/// <returns>The show.</returns>
			/// <param name="name">Name.</param>
			public LabUIElement Show(string name) {
				if (!_elementDic.ContainsKey(name)) return null;
				if (_curElement != null) {
					if (_curElement.name == name) {
						return _curElement;
					} else {
						_curElement.Hide();
					}
				}
				_curElement = _elementDic[name];
				_curElement.Show();
				return _curElement;
			}

			/// <summary>
			/// UI要素を非表示にする
			/// </summary>
			public void Hide() {
				if (_curElement != null) {
					_curElement.Hide();
					_curElement = null;
				}
			}

			/// <summary>
			/// 指定した名前のUI要素を取得する
			/// </summary>
			/// <returns>The get.</returns>
			/// <param name="name">Name.</param>
			public LabUIElement Get(string name) {
				if (_elementDic.ContainsKey(name)) {
					return _elementDic[name];
				}
				return null;
			}

			/// <summary>
			/// UI要素の非表示
			/// </summary>
			private void OnHide() {
				if (_curElement) {
					_curElement = null;
				}
			}
		}

		[Header("Event System")]

		/// <summary>
		/// UIのイベントシステム
		/// </summary>
		[SerializeField]
		private EventSystem _eventSystem;

		public EventSystem eventSystem {
			get {
				return _eventSystem;
			}
		}

		[Header("Button")]

		[SerializeField]
		private Button _nodeBtn;

		public Button nodeBtn {
			get {
				return _nodeBtn;
			}
		}

		[SerializeField]
		private Button _linkBtn;

		public Button linkBtn {
			get {
				return _linkBtn;
			}
		}

		[SerializeField]
		private Button _selectBtn;

		public Button selectBtn {
			get {
				return _selectBtn;
			}
		}

		[SerializeField]
		private Button _playBtn;

		public Button playBtn {
			get {
				return _playBtn;
			}
		}

		[Header("UIElement")]

		/// <summary>
		/// UIグループ
		/// </summary>
		[SerializeField]
		private LabUIGroup[] _groups;

		/// <summary>
		/// UIグループの辞書
		/// </summary>
		private Dictionary<string, LabUIGroup> _groupDic;

		private void Awake() {
			_groupDic = new Dictionary<string, LabUIGroup>();
		}

		private void Start() {
			for (int i = 0; i < _groups.Length; ++i) {
				_groupDic.Add(_groups[i].name, _groups[i]);
				_groups[i].Initialize();
			}
		}

		/// <summary>
		/// 指定した名前のUI要素を表示する
		/// </summary>
		/// <param name="name">Name.</param>
		public LabUIElement ShowUIElement(string groupName, string name) {
			if (_groupDic.ContainsKey(groupName)) {
				return _groupDic[groupName].Show(name);
			}
			return null;
		}

		/// <summary>
		/// 指定したグループのUI要素を非表示にする
		/// </summary>
		/// <param name="groupName">Group name.</param>
		public void HideUIElement(string groupName) {
			if (_groupDic.ContainsKey(groupName)) {
				_groupDic[groupName].Hide();
			}
		}

		/// <summary>
		/// 指定した名前のUI要素を取得する
		/// </summary>
		/// <param name="groupName">Group name.</param>
		/// <param name="name">Name.</param>
		public LabUIElement GetUIElement(string groupName, string name) {
			if (_groupDic.ContainsKey(groupName)) {
				return _groupDic[groupName].Get(name);
			}
			return null;
		}
	}
}