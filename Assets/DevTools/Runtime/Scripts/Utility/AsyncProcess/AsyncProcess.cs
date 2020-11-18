using UnityEngine;

namespace DevTools
{
	public class AsyncProcess : MonoBehaviour, IAsyncProcess
	{
		public class Factory
		{
			public static AsyncProcess Create(string name = null, Transform parent = null)
			{
				GameObject asyncProcessGo = new GameObject(string.IsNullOrEmpty(name) ? typeof(AsyncProcess).Name : name, typeof(AsyncProcess));
				asyncProcessGo.transform.parent = parent;

				return asyncProcessGo.GetComponent<AsyncProcess>();
			}

			public static AsyncProcess CreateDontDestroy(string name = null, Transform parent = null)
			{
				AsyncProcess asyncProcess = Create(name);
				asyncProcess.transform.parent = parent;

				DontDestroyOnLoad(asyncProcess.gameObject);

				return asyncProcess;
			}

			public static AsyncProcess Create(GameObject target)
			{
				return target.AddComponent<AsyncProcess>();
			}
		}
	}
}