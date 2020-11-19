using System;
using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new PackageExportSettings", menuName = "DevTools/Package Export Settings", order = 0)]
	public class PackageExportSettings : ScriptableObject
	{
		[Serializable]
		public class Settings
		{
			public string Id;
			public PackageExporter.PackageExportInfo ExportInfo;
		}

		[SerializeField]
		private Settings settings;

		public string Id => settings.Id;
		public PackageExporter.PackageExportInfo ExportInfo => settings.ExportInfo;

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}
	}
}