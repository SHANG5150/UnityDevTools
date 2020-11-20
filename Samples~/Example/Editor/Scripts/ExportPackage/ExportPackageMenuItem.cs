using DevTools.Editor;
using System.Linq;
using UnityEditor;

namespace DevTools.Example.Editor
{
	public class ExportPackageMenuItem
	{
		[MenuItem("Tools/Export Package/Dev Tools")]
		public static void ExportDevTools()
		{
			PackageExportSettings packageSettings = AssetUtility.FindScriptableObjectAssets<PackageExportSettings>(x => x.Id == "DevTools").First();

			var exporter = new PackageExporter(packageSettings.ExportInfo);

			exporter.Export();
		}
	}
}