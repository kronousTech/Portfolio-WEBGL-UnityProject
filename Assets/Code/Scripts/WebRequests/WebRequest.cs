using System;
using UnityEngine;
using UnityEngine.Networking;

namespace KronosTech.WebRequests
{
    public static class WebRequest
    {
        public static async void Get(string url, Action<WebRequestEventArgs> callback)
        {
            using var www = UnityWebRequest.Get(url);
            
            try
            {
                await www.SendWebRequest();
            }
            catch (Exception e)
            {
                Debug.LogError($"{nameof(WebRequest)}.cs: " +
                    $"Exception requesting {url}. \n" +
                    $"Message: {e.Message}.");

                callback?.Invoke(new WebRequestEventArgs(e.Message));
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log($"{nameof(WebRequest)}.cs: " +
                //    $"Successful request to {url}.");

                callback?.Invoke(new WebRequestEventArgs(www.downloadHandler));
            }
            else
            {
                Debug.LogError($"{nameof(WebRequest)}.cs: " +
                    $"Error getting data {www.result} : {url}. \n" +
                    $"Message: {www.error}.");

                callback?.Invoke(new WebRequestEventArgs(www.error));
            }
        }
    }
}

