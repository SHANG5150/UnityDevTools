using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DevTools
{
	public class BinaryPlayerPrefs : IPlayerPrefs
	{
		private readonly MemoryPlayerPrefs memoryPlayerPrefs = null;
		private readonly string filePath;

		public BinaryPlayerPrefs(string filePath)
		{
			this.filePath = filePath;

			if (File.Exists(filePath))
			{
				using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
				{
					BinaryFormatter fmt = new BinaryFormatter();
					memoryPlayerPrefs = fmt.Deserialize(fs) as MemoryPlayerPrefs;
					fs.Close();
				}
			}
			else
			{
				memoryPlayerPrefs = new MemoryPlayerPrefs();
			}
		}

		public void DeleteAll()
		{
			memoryPlayerPrefs.DeleteAll();
		}

		public void DeleteKey(string key)
		{
			memoryPlayerPrefs.DeleteKey(key);
		}

		public float GetFloat(string key, float defaultValue)
		{
			return memoryPlayerPrefs.GetFloat(key, defaultValue);
		}

		public float GetFloat(string key)
		{
			return memoryPlayerPrefs.GetFloat(key);
		}

		public int GetInt(string key, int defaultValue)
		{
			return memoryPlayerPrefs.GetInt(key, defaultValue);
		}

		public int GetInt(string key)
		{
			return memoryPlayerPrefs.GetInt(key);
		}

		public string GetString(string key, string defaultValue)
		{
			return memoryPlayerPrefs.GetString(key, defaultValue);
		}

		public string GetString(string key)
		{
			return memoryPlayerPrefs.GetString(key);
		}

		public bool HasKey(string key)
		{
			return memoryPlayerPrefs.HasKey(key);
		}

		public void Save()
		{
			using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
			{
				BinaryFormatter fmt = new BinaryFormatter();
				fmt.Serialize(fs, memoryPlayerPrefs);
				fs.Close();
			}
		}

		public void SetFloat(string key, float value)
		{
			memoryPlayerPrefs.SetFloat(key, value);
		}

		public void SetInt(string key, int value)
		{
			memoryPlayerPrefs.SetInt(key, value);
		}

		public void SetString(string key, string value)
		{
			memoryPlayerPrefs.SetString(key, value);
		}
	}
}