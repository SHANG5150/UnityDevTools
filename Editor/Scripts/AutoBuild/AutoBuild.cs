using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace DevTools.Editor
{
	public class AutoBuild
	{
		public class Factory
		{
			public static AutoBuild Create()
			{
				return new AutoBuild();
			}
		}

		public void Build(string id)
		{
			AutoBuildPipelineSettings settings = AssetUtility.FindScriptableObjectAssets<AutoBuildPipelineSettings>(x => x.id == id).First();

			Stopwatch stopwatch = Stopwatch.StartNew();
			StringBuilder buildLog = new StringBuilder();
			buildLog.Append($"Build [{settings.id}]");

			foreach (var buildProcess in settings.buildProcesses)
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