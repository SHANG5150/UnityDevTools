using UnityEngine;

namespace DevTools
{
	public class UnityPlayerPrefsFacade : IPlayerPrefs
	{
		public void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

		public void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}

		public float GetFloat(string key, float defaultValue)
		{
			return PlayerPrefs.GetFloat(key, defaultValue);
		}

		public float GetFloat(string key)
		{
			return PlayerPrefs.GetFloat(key);
		}

		public int GetInt(string key, int defaultValue)
		{
			return PlayerPrefs.GetInt(key, defaultValue);
		}

		public int GetInt(string key)
		{
			return PlayerPrefs.GetInt(key);
		}

		public string GetString(string key, string defaultValue)
		{
			return PlayerPrefs.GetString(key, defaultValue);
		}

		public string GetString(string key)
		{
			return PlayerPrefs.GetString(key);
		}

		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public void Save()
		{
			PlayerPrefs.Save();
		}

		public void SetFloat(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value);
		}

		public void SetInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
		}

		public void SetString(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}
	}
}