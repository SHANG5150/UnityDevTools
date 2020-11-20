using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace DevTools
{
	public class SymmetricKeyEncryption : ISymmetricKeyEncryption
	{
		public class Factory
		{
			public static SymmetricKeyEncryption Create()
			{
				return new SymmetricKeyEncryption(new Settings() { SaltLength = 32 });
			}
		}

		public class Settings
		{
			public int SaltLength;
		}

		private Settings settings;

		public SymmetricKeyEncryption(Settings settings)
		{
			this.settings = settings;
		}

		public byte[] Encrypt(byte[] content, byte[] key)
		{
			var sha256 = new SHA256CryptoServiceProvider();
			byte[] keyBytes = sha256.ComputeHash(key);
			byte[] iv = new byte[16];

			Array.Copy(keyBytes, iv, iv.Length);

			Aes aes = Aes.Create();
			aes.Key = keyBytes;
			aes.IV = iv;

			byte[] saltedContent = AddSalt(content);
			byte[] encrytedData = null;

			using (var ms = new MemoryStream())
			using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
			{
				cs.Write(saltedContent, 0, saltedContent.Length);
				cs.FlushFinalBlock();

				encrytedData = ms.ToArray();
			}

			return encrytedData;
		}

		public byte[] Decrypt(byte[] encryptedbytes, byte[] key)
		{
			var sha256 = new SHA256CryptoServiceProvider();
			byte[] keyBytes = sha256.ComputeHash(key);
			byte[] iv = new byte[16];

			Array.Copy(keyBytes, iv, iv.Length);

			Aes aes = Aes.Create();
			aes.Key = keyBytes;
			aes.IV = iv;

			byte[] saltedContent = null;

			using (var ms = new MemoryStream())
			using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
			{
				cs.Write(encryptedbytes, 0, encryptedbytes.Length);
				cs.FlushFinalBlock();

				saltedContent = ms.ToArray();
			}

			byte[] content = RemoveSalt(saltedContent);

			return content;
		}

		private byte[] AddSalt(byte[] content)
		{
			RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
			byte[] salt = new byte[settings.SaltLength];

			rngCsp.GetBytes(salt);

			return content.Concat(salt).ToArray();
		}

		private byte[] RemoveSalt(byte[] saltedContent)
		{
			byte[] content = new byte[saltedContent.Length - settings.SaltLength];

			Array.Copy(saltedContent, content, content.Length);

			return content;
		}
	}
}