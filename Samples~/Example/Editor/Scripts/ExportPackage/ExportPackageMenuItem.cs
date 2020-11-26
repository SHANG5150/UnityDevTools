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
			PackageExport packageExport = AssetUtility.FindScriptableObjectAssets<PackageExport>(x => x.Id == "DevTools").First();

			packageExport.Export();
		}
	}
}