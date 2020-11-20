using DevTools.Editor;
using System.Linq;
using UnityEditor;

namespace DevTools.Example.Editor
{
	public static class ReloadXmlDataMenuItem
	{
		[MenuItem("Tools/Reload Xml Data")]
		public static void ReloadXmlData()
		{
			ExcelXmlExtractionSettings settings = AssetUtility.FindScriptableObjectAssets<ExcelXmlExtractionSettings>(x => x.Id == "Example").First();

			Extract<SampleXmlData, SampleXmlData.Data>(settings, (container, data) => container.Construct(data));
		}

		private static void Extract<TContainer, TData>(ExcelXmlExtractionSettings settings, DataExtractor.Fill<TContainer, TData> fillMethod)
			where TContainer : class, new()
			where TData : class, new()
		{
			string containerTypeName = typeof(TContainer).Name;
			var xmlFile = settings[containerTypeName].XmlFile;
			var container = settings[containerTypeName].Container;
			EditorUtility.SetDirty(container);

			var excelXmlProcess = new ExcelXmlExtractionProcessor(xmlFile, container);

			DataExtractor extractor = new DataExtractor(excelXmlProcess);
			extractor.Extract(fillMethod);

			AssetDatabase.SaveAssets();
		}
	}
}