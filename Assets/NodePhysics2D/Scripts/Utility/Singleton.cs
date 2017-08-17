using UnityEngine;
using System.Collections;

namespace NodePhysics2D {

	/// <summary>
	/// シングルトン
	/// </summary>
	public abstract class Singleton<T> : MonoBehaviour where T : class {

		/// <summary>
		/// 保持するインスタンス
		/// </summary>
		private static T _instance;

		public static T instance {
			get {
				if (_instance == null) {
					T t = FindObjectOfType(typeof(T)) as T;
					if (t == null) {
						string tName = typeof(T).Name;
						Debug.LogWarning(string.Format("Missing {0} ! Create the singleton instance.", tName));
						GameObject g = new GameObject(tName, typeof(T));
						_instance = g.GetComponent<T>();
					} else {
						_instance = t;
					}
				}
				return _instance;
			}
		}

		/// <summary>
		/// 派生クラスではAwakeを定義してはいけない
		/// </summary>
		public void Awake() {
			if (_instance != null) {
				Destroy(gameObject);
			}
			SingletonAwake();
		}

		/// <summary>
		/// Awakeの代替
		/// Awakeを定義してはいけない
		/// </summary>
		public abstract void SingletonAwake();
	}
}