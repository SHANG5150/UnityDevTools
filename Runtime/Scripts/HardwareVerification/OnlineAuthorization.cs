using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace DevTools
{
	public class OnlineAuthorization
	{
		[Serializable]
		public class Settings
		{
			public string AuthUrl;
			public string OkResponse;
			public string KeyFieldName;
			[NonSerialized] public string Key;
			public string TagFieldName;
			public string Tag;
		}

		public delegate void OnFinishVerification(bool isOk);

		private readonly Settings settings;

		public OnlineAuthorization(Settings settings)
		{
			this.settings = settings;
		}

		public IEnumerator Verify(OnFinishVerification onFinish)
		{
			WWWForm form = new WWWForm();
			form.AddField(settings.KeyFieldName, settings.Key);
			form.AddField(settings.TagFieldName, settings.Tag);

			using (var webRequest = UnityWebRequest.Post(settings.AuthUrl, form))
			{
				yield return webRequest.SendWebRequest();

				if (webRequest.isNetworkError || webRequest.isHttpError)
				{
					Debug.LogError(webRequest.error);
					onFinish.Invoke(false);
				}
				else if (webRequest.downloadHandler.text == settings.OkResponse)
				{
					onFinish.Invoke(true);
				}
				else
				{
					onFinish.Invoke(false);
				}
			}
		}

		public async Task<bool> VerifyAsync()
		{
			bool isOk = false;

			WWWForm form = new WWWForm();
			form.AddField(settings.KeyFieldName, settings.Key);
			form.AddField(settings.TagFieldName, settings.Tag);

			using (var webRequest = UnityWebRequest.Post(settings.AuthUrl, form))
			{
				var request = webRequest.SendWebRequest();

				while (!request.isDone)
				{
					await Task.Yield();
				}

				if (webRequest.isNetworkError || webRequest.isHttpError)
				{
					Debug.Log(webRequest.error);
					isOk = false;
				}
				else if (webRequest.downloadHandler.text == settings.OkResponse)
				{
					isOk = true;
				}
			}

			return isOk;
		}
	}
}