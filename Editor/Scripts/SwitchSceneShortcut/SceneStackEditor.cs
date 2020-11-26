using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	[CustomEditor(typeof(SceneStack))]
	public class SceneStackEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Open"))
			{
				var sceneStack = target as SceneStack;

				sceneStack.OpenSceneStack();
			}
		}
	}
}