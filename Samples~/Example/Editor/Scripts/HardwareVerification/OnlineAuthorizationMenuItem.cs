using DevTools.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DevTools.Example.Editor
{
	public class OnlineAuthorizationMenuItem
	{
		private const string EXAMPLE_SETTINGS_ID = "Example";

		[MenuItem("Tools/Verify This Device")]
		public static async void RunOnlineVerificationAsync()
		{
			Debug.Log("Start online verification.");

			OnlineAuthorizationSettings authSetting = AssetUtility.FindScriptableObjectAssets<OnlineAuthorizationSettings>(x => x.Id == EXAMPLE_SETTINGS_ID).First();
			authSetting.authSettings.Key = SystemInfo.deviceUniqueIdentifier;

			OnlineAuthorization auth = new OnlineAuthorization(authSetting.authSettings);

			bool isOk = await auth.VerifyAsync();

			Debug.Log($"Online verification status: [{isOk}]");
		}
	}
}