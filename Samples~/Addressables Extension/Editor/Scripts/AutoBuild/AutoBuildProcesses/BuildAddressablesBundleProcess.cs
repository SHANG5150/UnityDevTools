using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new BuildAddressablesBundleProcess", menuName = "DevTools/AutoBuild/Processes/Build Addressables Bundle", order = 0)]
	public class BuildAddressablesBundleProcess : AutoBuildProcess
	{
		public override string ProcessName => typeof(BuildAddressablesBundleProcess).Name;

		public override void Process()
		{
			var dataBuilder = AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder;
			AddressableAssetSettings.CleanPlayerContent(dataBuilder);
			AddressableAssetSettings.BuildPlayerContent();
		}
	}
}