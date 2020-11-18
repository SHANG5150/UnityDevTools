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
			ExportPackageInfo packageInfo = AssetUtility.FindScriptableObjectAssets<ExportPackageInfo>(x => x.Id == "DevTools").First();

			var exporter = new PackageExporter(packageInfo.ExportInfo);

			exporter.Export();
		}
	}
}