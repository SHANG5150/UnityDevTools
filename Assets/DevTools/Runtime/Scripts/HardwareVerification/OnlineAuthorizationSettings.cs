using System;
using UnityEngine;

namespace DevTools
{
	[CreateAssetMenu(fileName = "new OnlineAuthorizationSettings", menuName = "DevTools/OnlineAuthorizationSettings", order = 0)]
	public class OnlineAuthorizationSettings : ScriptableObject
	{
		[Serializable]
		public class Settings
		{
			public string Id;
			public OnlineAuthorization.Settings authSettings;
		}

		[SerializeField]
		private Settings settings;

		public string Id => settings.Id;
		public OnlineAuthorization.Settings authSettings => settings.authSettings;

		public void Construct(Settings settings)
		{
			this.settings = settings;
		}
	}
}