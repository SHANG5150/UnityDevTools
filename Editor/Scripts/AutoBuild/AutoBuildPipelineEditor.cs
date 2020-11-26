using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	[CustomEditor(typeof(AutoBuildPipeline))]
	public class AutoBuildPipelineEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Space(5.0f);

			if (GUILayout.Button("Build"))
			{
				var autoBuildPipeline = target as AutoBuildPipeline;
				autoBuildPipeline.Build();
			}
		}
	}
}