using System;
using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new ExcelXmlExtractionSettings", menuName = "DevTools/Excel Xml Extraction Settings", order = 0)]
	public class ExcelXmlExtractionSettings : ScriptableObject
	{
		[Serializable]
		public class DataMap
		{
			public TextAsset XmlFile;
			public ScriptableObject Container;
		}

		[Serializable]
		public class Settings
		{
			public string Id;
			public DataMap[] DataMaps;
		}

		[SerializeField]
		private Settings settings;

		public string Id => settings.Id;
		public DataMap[] DataMaps => settings.DataMaps;

		public DataMap this[string typeName]
		{
			get
			{
				foreach (var dataMap in DataMaps)
				{
					if (dataMap.Container.GetType().Name == typeName)
					{
						return dataMap;
					}
				}

				return null;
			}
		}

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}
	}
}