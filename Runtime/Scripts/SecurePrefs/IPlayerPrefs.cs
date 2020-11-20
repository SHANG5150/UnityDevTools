namespace DevTools
{
	public interface IPlayerPrefs

	{
		void DeleteAll();

		void DeleteKey(string key);

		float GetFloat(string key, float defaultValue);

		float GetFloat(string key);

		int GetInt(string key, int defaultValue);

		int GetInt(string key);

		string GetString(string key, string defaultValue);

		string GetString(string key);

		bool HasKey(string key);

		void Save();

		void SetFloat(string key, float value);

		void SetInt(string key, int value);

		void SetString(string key, string value);
	}
}