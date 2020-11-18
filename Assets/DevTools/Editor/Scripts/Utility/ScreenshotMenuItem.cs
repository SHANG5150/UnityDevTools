using UnityEditor;

namespace DevTools.Editor
{
	public class ScreenshotMenuItem
	{
		[MenuItem("Tools/Save Screenshot")]
		public static void SaveScreenshot()
		{
			Screenshot screenshot = new Screenshot();

			screenshot.Save();
		}
	}
}