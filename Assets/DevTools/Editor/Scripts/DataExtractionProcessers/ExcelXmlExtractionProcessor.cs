using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using UnityEngine;

namespace DevTools.Editor
{
	public class ExcelXmlExtractionProcessor : IDataExtraction
	{
		private readonly ScriptableObject container;
		private readonly XDocument xDoc;
		private readonly string xDocNamespace;
		private readonly XName worksheet;
		private readonly XName table;
		private readonly XName row;
		private readonly XName cell;

		private delegate void SetValueAction(FieldInfo fieldInfo, object target, string vaule);

		private Dictionary<string, SetValueAction> setValueActions;

		public ExcelXmlExtractionProcessor(TextAsset xmlTextAsset, ScriptableObject container)
		{
			this.container = container;
			xDoc = XDocument.Parse(xmlTextAsset.text);
			xDocNamespace = xDoc.Root.GetNamespaceOfPrefix("ss").NamespaceName;
			worksheet = XName.Get("Worksheet", xDocNamespace);
			table = XName.Get("Table", xDocNamespace);
			row = XName.Get("Row", xDocNamespace);
			cell = XName.Get("Cell", xDocNamespace);

			setValueActions = GetSetValueActions();
		}

		private Dictionary<string, SetValueAction> GetSetValueActions()
		{
			Dictionary<string, SetValueAction> setValueActions = new Dictionary<string, SetValueAction>();
			setValueActions.Add(typeof(int).Name, (fieldInfo, target, vaule) => fieldInfo.SetValue(target, int.Parse(vaule)));
			setValueActions.Add(typeof(float).Name, (fieldInfo, target, vaule) => fieldInfo.SetValue(target, float.Parse(vaule)));
			setValueActions.Add(typeof(string).Name, (fieldInfo, target, value) => fieldInfo.SetValue(target, value));
			setValueActions.Add(typeof(bool).Name, (fieldInfo, target, value) =>
			{
				if (value == "1" || value == bool.TrueString)
				{
					fieldInfo.SetValue(target, true);
				}
				else if (value == "0" || value == bool.FalseString)
				{
					fieldInfo.SetValue(target, false);
				}
				else
				{
					throw new InvalidCastException($"Cannot cast value [{value}] to bool format for field [{fieldInfo.Name}].");
				}
			});

			return setValueActions;
		}

		public TContainer CreateContainer<TContainer>()
			where TContainer : class, new()
		{
			TContainer container = this.container as TContainer;

			return container;
		}

		public TData[] ExtractData<TData>()
			where TData : class, new()
		{
			string[] fieldNames;
			string[][] dataRows;

			GetDataHeaderAndRows(xDoc, out fieldNames, out dataRows);

			List<TData> dataList = new List<TData>();

			foreach (var dataRow in dataRows)
			{
				TData newData = new TData();

				foreach (var fieldName in fieldNames)
				{
					FieldInfo field = typeof(TData).GetField(fieldName);
					string value = dataRow[Array.IndexOf(fieldNames, fieldName)];

					setValueActions[field.FieldType.Name].Invoke(field, newData, value);
				}

				dataList.Add(newData);
			}

			return dataList.ToArray();
		}

		private void GetDataHeaderAndRows(XDocument xDoc, out string[] dataHeader, out string[][] dataRows)
		{
			dataHeader = GetHeaders(xDoc);
			dataRows = GetRowValues(xDoc);
		}

		private string[] GetHeaders(XDocument xDoc)
		{
			XElement[] headerElements = xDoc.Root.Element(worksheet).Element(table).Elements(row).Take(1).Elements(cell).ToArray();
			List<string> headers = new List<string>();

			foreach (var item in headerElements)
			{
				headers.Add(item.Value);
			}

			return headers.ToArray();
		}

		private string[][] GetRowValues(XDocument xDoc)
		{
			XElement[] rows = xDoc.Root.Element(worksheet).Element(table).Elements(row).Skip(1).ToArray();
			string[][] rowValues = new string[rows.Length][];

			for (int i = 0; i < rowValues.Length; i++)
			{
				XElement[] rowCells = rows[i].Elements(cell).ToArray();
				rowValues[i] = new string[rowCells.Length];

				for (int j = 0; j < rowCells.Length; j++)
				{
					rowValues[i][j] = rowCells[j].Value;
				}
			}

			return rowValues;
		}
	}
}