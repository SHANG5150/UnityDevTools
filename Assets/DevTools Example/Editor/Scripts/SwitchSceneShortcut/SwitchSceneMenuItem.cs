using DevTools.Editor;
using UnityEditor;

namespace DevTools.Example.Editor
{
	/// <summary>
	/// A demonstration for use SceneStack to switch scenes.
	/// </summary>
	public class SwitchSceneMenuItem
	{
		[MenuItem("Tools/Open Scene Shortcut/Startup")]
		public static void OpenStartup()
		{
			SceneStack.Factory.Create().OpenSceneStack("Startup");
		}

		[MenuItem("Tools/Open Scene Shortcut/Environment")]
		public static void OpenEnvironment()
		{
			SceneStack.Factory.Create().OpenSceneStack("Environment");
		}

		[MenuItem("Tools/Open Scene Shortcut/Gameplay")]
		public static void OpenGamePlay()
		{
			SceneStack.Factory.Create().OpenSceneStack("Gameplay");
		}
	}
}