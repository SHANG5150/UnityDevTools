using System.Linq;

namespace DevTools.Editor
{
	public class AutoBuild
	{
		public class Factory
		{
			public static AutoBuild Create()
			{
				return new AutoBuild();
			}
		}

		public void Build(string id)
		{
			AutoBuildPipeline buildPipeline = AssetUtility.FindScriptableObjectAssets<AutoBuildPipeline>(x => x.Id == id).First();

			buildPipeline.Build();
		}
	}
}