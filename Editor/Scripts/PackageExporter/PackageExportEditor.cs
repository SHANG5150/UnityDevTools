using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	[CustomEditor(typeof(PackageExport))]
	public class PackageExportEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Space(5.0f);

			if (GUILayout.Button("Export"))
			{
				var packageExport = target as PackageExport;

				packageExport.Export();
			}
		}
	}
}