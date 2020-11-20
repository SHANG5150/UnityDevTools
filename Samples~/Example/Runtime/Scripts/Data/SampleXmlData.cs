using System;
using UnityEngine;

namespace DevTools.Example
{
	[CreateAssetMenu(fileName = "new SampleXmlData", menuName = "DevTools/Data/Sample Xml Data", order = 0)]
	[PreferBinarySerialization]
	public class SampleXmlData : ScriptableObject
	{
		[Serializable]
		public class Data
		{
			public string Id;
			public int IntValue;
			public float FloatValue;
			public bool BooleanValue;
		}

		[SerializeField]
		private Data[] dataRows;

		public Data[] data => dataRows;

		public void Construct(Data[] dataRows)
		{
			this.dataRows = dataRows;
		}
	}
}