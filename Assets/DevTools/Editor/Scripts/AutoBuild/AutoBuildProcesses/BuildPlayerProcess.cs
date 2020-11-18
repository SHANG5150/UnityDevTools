using System;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new BuildPlayerProcess", menuName = "DevTools/AutoBuild/Processes/Build Player", order = 0)]
	public class BuildPlayerProcess : AutoBuildProcess
	{
		[Serializable]
		public class Settings
		{
			public bool IsDevBuild;
			public bool ShowBuiltPlayer;
			public string AppFileName;
			public BuildTargetGroup TargetGroup;
			public BuildTarget Target;
		}

		[SerializeField]
		private Settings settings;

		public override string ProcessName => typeof(BuildPlayerProcess).Name;

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}

		public override void Process()
		{
			BuildPlayerOptions buildPlayerOptions = GetBuildPlayerOptions(settings);

			BuildPipeline.BuildPlayer(buildPlayerOptions);
		}

		private BuildPlayerOptions GetBuildPlayerOptions(Settings settings)
		{
			string[] buildScenes = new string[] { EditorBuildSettings.scenes.First().path };
			StringBuilder locationPathBuilder = new StringBuilder();
			{
				locationPathBuilder.Append($"{Environment.CurrentDirectory}/Build/");
				locationPathBuilder.Append(settings.IsDevBuild ? "Dev" : "Release").Append("/");
				locationPathBuilder.Append($"{DateTime.Now:yyyy_MMdd_HHmm}/{settings.AppFileName}");
			}
			string buildLocationPath = locationPathBuilder.ToString();
			BuildOptions buildOptions = BuildOptions.None;
			{
				buildOptions |= settings.IsDevBuild ? BuildOptions.Development : BuildOptions.None;
				buildOptions |= settings.ShowBuiltPlayer ? BuildOptions.ShowBuiltPlayer : BuildOptions.None;
			}

			BuildPlayerOptions playerOptions = new BuildPlayerOptions()
			{
				scenes = buildScenes,
				locationPathName = buildLocationPath,
				targetGroup = settings.TargetGroup,
				target = settings.Target,
				options = buildOptions
			};

			return playerOptions;
		}
	}
}