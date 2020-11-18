using DevTools.Editor;
using System;
using UnityEngine;

namespace DevTools.Example.Editor
{
	[CreateAssetMenu(fileName = "new ExportPackageInfo", menuName = "DevTools/Export Package Info", order = 0)]
	public class ExportPackageInfo : ScriptableObject
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