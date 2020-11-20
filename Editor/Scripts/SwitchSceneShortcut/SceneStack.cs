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
			public static SceneStack Create(string id)
			{
				return AssetUtility.FindScriptableObjectAssets<SceneStack>(x => x.settings.Id == id).First();
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
		private Settings settings;

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}

		public void OpenSceneStack()
		{
			string singleScenePath = AssetDatabase.GetAssetPath(settings.Single);

			EditorSceneManager.OpenScene(singleScenePath, OpenSceneMode.Single);

			foreach (var additiveScene in settings.AdditiveScenes)
			{
				string additiveScenePath = AssetDatabase.GetAssetPath(additiveScene);
				EditorSceneManager.OpenScene(additiveScenePath, OpenSceneMode.Additive);
			}
		}
	}
}