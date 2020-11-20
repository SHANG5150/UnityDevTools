using System;
using System.Collections.Generic;

namespace DevTools
{
	[Serializable]
	public class MemoryPlayerPrefs : IPlayerPrefs
	{
		private Dictionary<string, int> intPrefs = new Dictionary<string, int>();
		private Dictionary<string, float> floatPrefs = new Dictionary<string, float>();
		private Dictionary<string, string> stringPrefs = new Dictionary<string, string>();

		public void DeleteAll()
		{
			intPrefs.Clear();
			floatPrefs.Clear();
			stringPrefs.Clear();
		}

		public void DeleteKey(string key)
		{
			intPrefs.Remove(key);
			floatPrefs.Remove(key);
			stringPrefs.Remove(key);
		}

		public float GetFloat(string key, float defaultValue)
		{
			if (floatPrefs.TryGetValue(key, out float value))
			{
				return value;
			}
			else
			{
				return defaultValue;
			}
		}

		public float GetFloat(string key)
		{
			return GetFloat(key, default);
		}

		public int GetInt(string key, int defaultValue)
		{
			if (intPrefs.TryGetValue(key, out int value))
			{
				return value;
			}
			else
			{
				return defaultValue;
			}
		}

		public int GetInt(string key)
		{
			return GetInt(key, default);
		}

		public string GetString(string key, string defaultValue)
		{
			if (stringPrefs.TryGetValue(key, out string value))
			{
				return value;
			}
			else
			{
				return defaultValue;
			}
		}

		public string GetString(string key)
		{
			return GetString(key, default);
		}

		public bool HasKey(string key)
		{
			if (intPrefs.ContainsKey(key))
			{
				return true;
			}
			else if (floatPrefs.ContainsKey(key))
			{
				return true;
			}
			else if (stringPrefs.ContainsKey(key))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual void Save()
		{
			// Do nothings. MemoryPlayerPrefs data only available while object is alive.
		}

		public void SetFloat(string key, float value)
		{
			floatPrefs[key] = value;
		}

		public void SetInt(string key, int value)
		{
			intPrefs[key] = value;
		}

		public void SetString(string key, string value)
		{
			stringPrefs[key] = value;
		}
	}
}