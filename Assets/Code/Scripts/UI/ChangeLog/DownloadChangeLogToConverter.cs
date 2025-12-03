using KronosTech.WebRequests;
using UnityEngine;

namespace KronosTech.UI.ChangeLog
{
    public class DownloadChangeLogToConverter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ConvertChangeLogToTMP m_converter;

#if UNITY_EDITOR
        private readonly string m_developmentChangeLogURL = "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-UnityProject/refs/heads/develop/CHANGELOG.md";
#else
        private readonly string m_mainChangeLogURL = "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-UnityProject/refs/heads/main/CHANGELOG.md";
#endif

        private void Awake()
        {
#if UNITY_EDITOR
            WebRequest.Get(m_developmentChangeLogURL, AddTextToConverterCallback);
#else
            WebRequest.Get(m_mainChangeLogURL, AddTextToConverterCallback);
#endif
        }

        private void AddTextToConverterCallback(WebRequestEventArgs args)
        {
            if (!args.IsSuccessful)
            {
                m_converter.SetChangeLog("# Failed to download ChangeLog.");

                Debug.LogError($"{nameof(DownloadChangeLogToConverter)}.cs: " +
                    $"Failed to download ChangeLog.");

                return;
            }

            m_converter.SetChangeLog(args.Handler.text);
        }
    }
}