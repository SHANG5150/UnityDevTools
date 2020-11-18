using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new SceneStackSettings", menuName = "DevTools/SceneStack Settings", order = 0)]
	public class SceneStack : ScriptableObject
	{
		public class Factory
		{
			public static SceneStack Create()
			{
				return AssetUtility.FindScriptableObjectAsset<SceneStack>();
			}
		}

		[Serializable]
		public class Settings
		{
			public string Id;
			public SceneAsset Single;
			public List<SceneAsset> AdditiveScenes;
		}

		[SerializeField]
		private List<Settings> settingsGroup;

		public void Construct(List<Settings> settingsGroup)
		{
			this.settingsGroup = settingsGroup;
		}

		public void OpenSceneStack(string id)
		{
			Settings settings = GetSettings(id);

			string singleScenePath = AssetDatabase.GetAssetPath(settings.Single);

			EditorSceneManager.OpenScene(singleScenePath, OpenSceneMode.Single);

			foreach (var additiveScene in settings.AdditiveScenes)
			{
				string additiveScenePath = AssetDatabase.GetAssetPath(additiveScene);
				EditorSceneManager.OpenScene(additiveScenePath, OpenSceneMode.Additive);
			}
		}

		private Settings GetSettings(string id)
		{
			var sceneGroup = settingsGroup.Where(x => x.Id == id).First();

			return sceneGroup;
		}
	}
}