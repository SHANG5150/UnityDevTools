using System;
using System.Collections.Generic;
using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new AutoBuildPipeline", menuName = "DevTools/AutoBuild/Pipeline Settings", order = 0)]
	public class AutoBuildPipelineSettings : ScriptableObject
	{
		[Serializable]
		public class Settings
		{
			public string Id;
			public List<AutoBuildProcess> Processes;
		}

		[SerializeField]
		private Settings settings;

		public string id => settings.Id;

		public IEnumerable<AutoBuildProcess> buildProcesses => settings.Processes;

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}
	}
}