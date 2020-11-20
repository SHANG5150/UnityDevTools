using DevTools.Editor;
using UnityEditor;

namespace DevTools.Example.Editor
{
	/// <summary>
	/// A demonstration for use AutoBuild to build application by different settings.
	/// </summary>
	public class AutoBuildMenuItem
	{
		[MenuItem(itemName: "Tools/Build/PC Development")]
		public static void BuildPcDevelopment()
		{
			AutoBuild.Factory.Create().Build("PCDevBuildPipeline");
		}

		[MenuItem(itemName: "Tools/Build/PC Release")]
		public static void BuildPcRelease()
		{
			AutoBuild.Factory.Create().Build("PCReleaseBuildPipeline");
		}

		[MenuItem(itemName: "Tools/Build/Android Development")]
		public static void BuildAndroidDevelopment()
		{
			AutoBuild.Factory.Create().Build("AndroidDevBuildPipeline");
		}

		[MenuItem(itemName: "Tools/Build/Android Release")]
		public static void BuildAndroidRelease()
		{
			AutoBuild.Factory.Create().Build("AndroidReleaseBuildPipeline");
		}
	}
}
