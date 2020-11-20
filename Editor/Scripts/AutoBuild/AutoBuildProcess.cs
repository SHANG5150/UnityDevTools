using UnityEngine;

namespace DevTools.Editor
{
	public abstract class AutoBuildProcess : ScriptableObject
	{
		public abstract string ProcessName { get; }

		public abstract void Process();
	}
}