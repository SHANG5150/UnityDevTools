using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace DevTools.Editor
{
	public class PlayModeMenuItem
	{
		private const string _menuItemPath = "Tools/Play";
		private const int _menuItemPriority = -100;

		[MenuItem(itemName: _menuItemPath, isValidateFunction: false, priority: _menuItemPriority)]
		public static void EnterPlayMode()
		{
			if (EditorBuildSettings.scenes.Length == 0)
			{
				Debug.LogError("Please add least one scene in to Scene In Build list in Build Settings.");
				return;
			}

			var firstScene = EditorBuildSettings.scenes.First();

			EditorSceneManager.OpenScene(firstScene.path);

			EditorApplication.isPlaying = true;
		}

		[MenuItem(itemName: _menuItemPath, isValidateFunction: true, priority: _menuItemPriority)]
		public static bool EnterPlayModeValidate()
		{
			return !EditorApplication.isPlaying;
		}
	}
}