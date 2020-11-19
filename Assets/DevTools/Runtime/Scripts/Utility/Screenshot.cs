using System;
using System.IO;
using UnityEngine;

namespace DevTools
{
	public class Screenshot
	{
		private readonly int superSize;
		private readonly FileInfo fileInfo;

		public Screenshot() : this(folderPath: Path.Combine(Application.persistentDataPath, "Screenshots"))
		{
		}

		public Screenshot(string folderPath) : this(folderPath: folderPath, 1)
		{
		}

		public Screenshot(string folderPath, int superSize)
		{
			this.superSize = superSize;

			string filePath = Path.Combine(folderPath, $"image_{DateTime.Now:yyyy_MMdd_HHmmss}.PNG");
			this.fileInfo = new FileInfo(filePath);
		}

		public void Save()
		{
			if (!fileInfo.Directory.Exists)
			{
				fileInfo.Directory.Create();
			}

			ScreenCapture.CaptureScreenshot(fileInfo.FullName, superSize);

			Debug.Log($"Save screenshot at: {fileInfo.FullName}");
		}
	}
}