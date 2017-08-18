using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// シミュレーションの管理クラス
	/// </summary>
	public class SimManager : Singleton<SimManager> {

		/// <summary>
		/// シミュレーションの設定
		/// </summary>
		[SerializeField]
		private SimSettings _settings;

		public SimSettings settings {
			get {
				return _settings;
			}
		}

		/// <summary>
		/// Awakeの代替
		/// Awakeを定義してはいけない
		/// </summary>
		public override void SingletonAwake() {
			_settings = ScriptableObject.CreateInstance<SimSettings>();
		}
	}
}