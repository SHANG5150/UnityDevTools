using System;
using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new SwitchPlatformProcess", menuName = "DevTools/AutoBuild/Processes/Switch Platform", order = 0)]
	public class SwitchPlatformProcess : AutoBuildProcess
	{
		[Serializable]
		public class Settings
		{
			public BuildTargetGroup TargetGroup;
			public BuildTarget Target;
		}

		[SerializeField]
		private Settings settings;

		public override string ProcessName => typeof(SwitchPlatformProcess).Name;

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}

		public override void Process()
		{
			EditorUserBuildSettings.SwitchActiveBuildTarget(settings.TargetGroup, settings.Target);
		}
	}
}