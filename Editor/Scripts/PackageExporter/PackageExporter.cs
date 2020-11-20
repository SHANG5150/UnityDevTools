using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	public class PackageExporter
	{
		[Serializable]
		public class PackageExportInfo
		{
			public string PackageName;
			public bool IncludeProjectVersion;
			public string[] PackageAssetsPath;
			public bool OpenFolderAfterExport;
		}

		private PackageExportInfo exportInfo;

		public PackageExporter(PackageExportInfo exportInfo)
		{
			this.exportInfo = exportInfo;
		}

		public void Export()
		{
			string outputFolder = Path.Combine(Environment.CurrentDirectory, "Export", $"{DateTime.Now:yyyy_MMdd_HHmm}");
			string packageName = exportInfo.PackageName + (exportInfo.IncludeProjectVersion ? $"_{Application.version}.unitypackage" : ".unitypackage");
			string outputPath = Path.Combine(outputFolder, packageName);

			FileInfo outputFileInfo = new FileInfo(outputPath);

			if (!outputFileInfo.Directory.Exists)
			{
				outputFileInfo.Directory.Create();
			}

			AssetDatabase.ExportPackage(exportInfo.PackageAssetsPath, outputPath, ExportPackageOptions.Recurse);

			if (exportInfo.OpenFolderAfterExport)
			{
				Application.OpenURL($"file:///{outputFileInfo.Directory.FullName}");
			}

			Debug.Log($"Export package finish: {outputFileInfo.FullName}");
		}
	}
}