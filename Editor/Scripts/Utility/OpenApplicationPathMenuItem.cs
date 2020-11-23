using System.IO;
using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
	public class OpenApplicationPathMenuItem
	{
		[MenuItem("Tools/Open Application Path/Data Path")]
		public static void OpenDataPath()
		{
			Application.OpenURL($"file:///{Application.dataPath}");
		}

		[MenuItem("Tools/Open Application Path/Persistent Data Path")]
		public static void OpenPersistentPath()
		{
			Application.OpenURL($"file:///{Application.persistentDataPath}");
		}

		[MenuItem("Tools/Open Application Path/Console Log Path")]
		public static void OpenConsoleLogPath()
		{
			FileInfo info = new FileInfo(Application.consoleLogPath);

			Application.OpenURL($"file:///{info.Directory.FullName}");
		}

		[MenuItem("Tools/Open Application Path/Temporary Cache Path")]
		public static void OpenTemporaryCachePath()
		{
			Application.OpenURL($"file:///{Application.temporaryCachePath}");
		}
	}
}