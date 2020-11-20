using System.Text;
using UnityEngine;

namespace DevTools
{
	public partial class SecurePrefs
	{
		public partial class Factory
		{
			public static SecurePrefs Create()
			{
				ISymmetricKeyEncryption cryptoService = SymmetricKeyEncryption.Factory.Create();
				Settings settings = new Settings()
				{
					Encoder = Encoding.Default,
					EncryptKey = Encoding.Default.GetBytes(SystemInfo.deviceUniqueIdentifier)
				};

				return new SecurePrefs(settings, cryptoService, new UnityPlayerPrefsFacade());
			}

			public static SecurePrefs CreateInMemory()
			{
				ISymmetricKeyEncryption cryptoService = SymmetricKeyEncryption.Factory.Create();
				Settings settings = new Settings()
				{
					Encoder = Encoding.Default,
					EncryptKey = Encoding.Default.GetBytes(SystemInfo.deviceUniqueIdentifier)
				};

				return new SecurePrefs(settings, cryptoService, new MemoryPlayerPrefs());
			}

			public static SecurePrefs CreateBinaryPrefs(string filePath)
			{
				ISymmetricKeyEncryption cryptoService = SymmetricKeyEncryption.Factory.Create();
				Settings settings = new Settings()
				{
					Encoder = Encoding.Default,
					EncryptKey = Encoding.Default.GetBytes(SystemInfo.deviceUniqueIdentifier)
				};

				return new SecurePrefs(settings, cryptoService, new BinaryPlayerPrefs(filePath));
			}

			public static SecurePrefs CreateBinaryPrefs()
			{
				return CreateBinaryPrefs($"{Application.persistentDataPath}/PlayerPrefs.dat");
			}
		}
	}
}