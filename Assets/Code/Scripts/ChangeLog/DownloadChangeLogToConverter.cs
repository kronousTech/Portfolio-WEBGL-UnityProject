using KronosTech.WebRequests;
using System.IO;
using UnityEngine;

namespace KronosTech.ChangeLog
{
    public class DownloadChangeLogToConverter : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ConvertChangeLogToTMP m_converter;

        private readonly string m_fileName = "CHANGELOG.md";

#if !UNITY_EDITOR
        private readonly string m_buildChangeLogURL = "https://raw.githubusercontent.com/kronousTech/Portfolio-WEBGL-Build/refs/heads/main/CHANGELOG.md";
#endif

        private void Awake()
        {
#if UNITY_EDITOR
            GetChangeLogFromProjectRoot();
#else
            WebRequest.Get(m_buildChangeLogURL, AddTextToConverterCallback);
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

        private void GetChangeLogFromProjectRoot()
        {
            var sourceFilePath = Path.Combine(
                Directory.GetParent(Application.dataPath).FullName,
                m_fileName
            );

            m_converter.SetChangeLog(File.ReadAllText(sourceFilePath));
        }
    }
}