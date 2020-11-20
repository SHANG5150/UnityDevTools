using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	public static class AssetUtility
	{
		public static ScriptableObject FindScriptableObjectAsset(Type type)
		{
			string[] assetGuids = AssetDatabase.FindAssets($"t:{type.Name}");

			if (assetGuids.Length == 0)
			{
				return null;
			}
			else if (assetGuids.Length > 1)
			{
				string ambiguousMatchMessage = string.Empty;

				foreach (var guid in assetGuids)
				{
					ambiguousMatchMessage += $"{AssetDatabase.GUIDToAssetPath(guid)};";
				}

				throw new AmbiguousMatchException(ambiguousMatchMessage);
			}

			string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[0]);
			ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);

			return asset;
		}

		public static T FindScriptableObjectAsset<T>()
			where T : ScriptableObject
		{
			return FindScriptableObjectAsset(typeof(T)) as T;
		}

		public static T[] FindScriptableObjectAssets<T>()
			where T : ScriptableObject
		{
			return FindScriptableObjectAssets<T>(null);
		}

		public static T[] FindScriptableObjectAssets<T>(Predicate<T> match)
			where T : ScriptableObject
		{
			string[] assetGuids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");

			if (assetGuids.Length == 0)
			{
				return null;
			}

			List<T> assets = new List<T>(assetGuids.Length);

			foreach (var assetGuid in assetGuids)
			{
				string assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
				T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

				if (match == null)
				{
					assets.Add(asset);
				}
				else if (match(asset))
				{
					assets.Add(asset);
				}
			}

			return assets.ToArray();
		}
	}
}