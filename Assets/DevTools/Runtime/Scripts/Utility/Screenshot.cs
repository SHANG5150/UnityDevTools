using System;
using System.IO;
using UnityEngine;

namespace DevTools
{
	public class Screenshot
	{
		private string folderPath;
		private int superSize;

		public Screenshot() : this(folderPath: Path.Combine(Environment.CurrentDirectory, "Screenshots"))
		{
		}

		public Screenshot(string folderPath) : this(folderPath: folderPath, 1)
		{
		}

		public Screenshot(string folderPath, int superSize)
		{
			this.folderPath = folderPath;
			this.superSize = superSize;
		}

		public void Save()
		{
			string filePath = Path.Combine(folderPath, $"image_{DateTime.Now:yyyy_MMdd_HHmmss}.PNG");
			FileInfo fileInfo = new FileInfo(filePath);

			if (!fileInfo.Directory.Exists)
			{
				fileInfo.Directory.Create();
			}

			ScreenCapture.CaptureScreenshot(filePath, superSize);

			Debug.Log($"Save screenshot at: {fileInfo.FullName}");
		}
	}
}