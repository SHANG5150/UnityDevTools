using System;
using System.Text;

namespace DevTools
{
	public partial class SecurePrefs
	{
		public class Settings
		{
			public Encoding Encoder;
			public byte[] EncryptKey;
		}

		private readonly Settings settings;
		private readonly ISymmetricKeyEncryption cryptoService;
		private readonly IPlayerPrefs playerPrefs;

		public SecurePrefs(Settings settings, ISymmetricKeyEncryption cryptoService, IPlayerPrefs playerPrefs)
		{
			this.settings = settings;
			this.cryptoService = cryptoService;
			this.playerPrefs = playerPrefs;
		}

		public void DeleteAll()
		{
			playerPrefs.DeleteAll();
		}

		public void DeleteKey(string key)
		{
			playerPrefs.DeleteKey(key);
		}

		public float GetFloat(string key, float defaultValue)
		{
			if (TryGetDecryptedVauleBytes(key, out byte[] valueBytes))
			{
				return BitConverter.ToSingle(valueBytes, 0);
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
			if (TryGetDecryptedVauleBytes(key, out byte[] valueBytes))
			{
				return BitConverter.ToInt32(valueBytes, 0);
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
			if (TryGetDecryptedVauleBytes(key, out byte[] valueBytes))
			{
				return settings.Encoder.GetString(valueBytes);
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
			return playerPrefs.HasKey(key);
		}

		public void Save()
		{
			playerPrefs.Save();
		}

		public void SetFloat(string key, float value)
			=> SetPrefs(key, cryptoService.Encrypt(BitConverter.GetBytes(value), settings.EncryptKey));

		public void SetInt(string key, int value)
			=> SetPrefs(key, cryptoService.Encrypt(BitConverter.GetBytes(value), settings.EncryptKey));

		public void SetString(string key, string value)
			=> SetPrefs(key, cryptoService.Encrypt(settings.Encoder.GetBytes(value), settings.EncryptKey));

		private void SetPrefs(string key, byte[] encrytedValueBytes)
		{
			string encrytedValue = Convert.ToBase64String(encrytedValueBytes);

			playerPrefs.SetString(key, encrytedValue);
		}

		private bool TryGetDecryptedVauleBytes(string key, out byte[] valueBytes)
		{
			valueBytes = null;

			string encrytedValue = playerPrefs.GetString(key, string.Empty);

			if (encrytedValue != string.Empty)
			{
				valueBytes = cryptoService.Decrypt(Convert.FromBase64String(encrytedValue), settings.EncryptKey);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}