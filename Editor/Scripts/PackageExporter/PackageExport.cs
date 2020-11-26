using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new PackageExportSettings", menuName = "DevTools/Package Export Settings", order = 0)]
	public class PackageExport : ScriptableObject
	{
		[Serializable]
		public class Settings
		{
			public string Id;
			public string PackageName;
			public bool IncludeProjectVersion;
			public string[] PackageAssetsPath;
			public bool OpenFolderAfterExport;
		}

		[SerializeField]
		private Settings settings;

		public string Id => settings.Id;

		public Settings ExportSettings => settings;

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}

		public static void Export(string id)
		{
			PackageExport packageExport = AssetUtility.FindScriptableObjectAssets<PackageExport>(x => x.Id == id).First();
			packageExport.Export();
		}

		[ContextMenu("Export")]
		public void Export()
		{
			string outputFolder = Path.Combine(Environment.CurrentDirectory, "Export", $"{DateTime.Now:yyyy_MMdd_HHmm}");
			string packageName = settings.PackageName + (settings.IncludeProjectVersion ? $"_{Application.version}.unitypackage" : ".unitypackage");
			string outputPath = Path.Combine(outputFolder, packageName);

			FileInfo outputFileInfo = new FileInfo(outputPath);

			if (!outputFileInfo.Directory.Exists)
			{
				outputFileInfo.Directory.Create();
			}

			AssetDatabase.ExportPackage(settings.PackageAssetsPath, outputPath, ExportPackageOptions.Recurse);

			if (settings.OpenFolderAfterExport)
			{
				Application.OpenURL($"file:///{outputFileInfo.Directory.FullName}");
			}

			Debug.Log($"Export package finish: {outputFileInfo.FullName}");
		}
	}
}