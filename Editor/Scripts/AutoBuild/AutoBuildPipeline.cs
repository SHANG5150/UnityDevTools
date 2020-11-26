using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new AutoBuildPipeline", menuName = "DevTools/AutoBuild/Pipeline Settings", order = 0)]
	public class AutoBuildPipeline : ScriptableObject
	{
		[Serializable]
		public class Settings
		{
			public string Id;
			public List<AutoBuildProcess> Processes;
		}

		[SerializeField]
		private Settings settings;

		public string Id => settings.Id;

		public IEnumerable<AutoBuildProcess> BuildProcesses => settings.Processes;

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}

		[ContextMenu("Build")]
		public void Build()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			StringBuilder buildLog = new StringBuilder();
			buildLog.Append($"Build [{Id}]");

			foreach (var buildProcess in BuildProcesses)
			{
				buildLog.Append($", {buildProcess.ProcessName}: ");
				double timestamp = stopwatch.Elapsed.TotalSeconds;

				buildProcess.Process();

				double timeSpended = stopwatch.Elapsed.TotalSeconds - timestamp;
				buildLog.Append($"[{timeSpended:F2}]");
			}

			stopwatch.Stop();
			buildLog.Append($", Total: [{stopwatch.Elapsed.TotalSeconds:F2}].");

			AssetDatabase.Refresh();

			Debug.Log(buildLog.ToString());
		}
	}
}