using NUnit.Framework;
using System;
using System.IO;
using UnityEngine;

namespace DevTools.Tests
{
	public class SecurePrefsTests
	{
		[Test]
		public void SetValueAndGetBackTests()
		{
			SecurePrefs securePrefs = SecurePrefs.Factory.Create();

			securePrefs.SetInt("IntKey", 100);
			Assert.AreEqual(securePrefs.GetInt("IntKey"), 100);

			securePrefs.DeleteKey("IntKey");
			Assert.IsFalse(securePrefs.HasKey("IntKey"));

			securePrefs.SetFloat("FloatKey", 0.333f);
			Assert.AreEqual(securePrefs.GetFloat("FloatKey"), 0.333f);

			securePrefs.SetString("StringKey", "Hello World!");
			Assert.AreEqual(securePrefs.GetString("StringKey"), "Hello World!");

			securePrefs.DeleteKey("IntKey");
			securePrefs.DeleteKey("FloatKey");
			securePrefs.DeleteKey("StringKey");
		}

		[Test]
		public void PrefsValueEncryptionTest()
		{
			SecurePrefs securePrefs = SecurePrefs.Factory.Create();

			securePrefs.SetInt("IntKey", 100);

			Assert.IsTrue(securePrefs.HasKey("IntKey"));
			Assert.AreNotEqual(PlayerPrefs.GetInt("IntKey"), 100);

			securePrefs.DeleteKey("IntKey");
		}

		[Test]
		public void BinarySecurePrefsTest()
		{
			string filePath = $"{Environment.CurrentDirectory}/PlayerPrefs_Test.bin";
			SecurePrefs securePrefs = SecurePrefs.Factory.CreateBinaryPrefs(filePath);

			securePrefs.SetInt("IntKey", 100);
			securePrefs.Save();

			SecurePrefs newSecurePrefs = SecurePrefs.Factory.CreateBinaryPrefs(filePath);

			Assert.AreEqual(newSecurePrefs.GetInt("IntKey"), 100);

			File.Delete(filePath);
		}
	}
}