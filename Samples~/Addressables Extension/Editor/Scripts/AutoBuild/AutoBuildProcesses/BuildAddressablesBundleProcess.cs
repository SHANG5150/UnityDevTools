#if USE_ADDRESSABLES

using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

#endif

using UnityEngine;

namespace DevTools.Editor
{
	[CreateAssetMenu(fileName = "new BuildAddressablesBundleProcess", menuName = "DevTools/AutoBuild/Processes/Build Addressables Bundle", order = 0)]
	public class BuildAddressablesBundleProcess : AutoBuildProcess
	{
		public override string ProcessName => typeof(BuildAddressablesBundleProcess).Name;

		public override void Process()
		{
#if USE_ADDRESSABLES
			var dataBuilder = AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder;
			AddressableAssetSettings.CleanPlayerContent(dataBuilder);
			AddressableAssetSettings.BuildPlayerContent();
#else
			Debug.LogWarning("BuildAddressablesBundle Process will not perform, please add [USE_ADDRESSABLES] to Scripting Define Symbols.");
#endif
		}
	}
}