using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.AssetBundles
{
    public class PresenterAssetBundlesDownloadProgress : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator m_animator;
        [SerializeField] private AssetBundleDownloadAll m_downloader;
        [SerializeField] private Slider m_progressBar;

        private readonly string m_triggerParameter = "Play";

        private void OnEnable()
        {
            m_downloader.OnDownloadProgress += UpdateProgressBarCallback;
            m_downloader.OnAllBundlesDownloaded += PlayFadeoutAnimationCallback;
        }
        private void OnDisable()
        {
            m_downloader.OnDownloadProgress -= UpdateProgressBarCallback;
            m_downloader.OnAllBundlesDownloaded -= PlayFadeoutAnimationCallback;
        }

        private void UpdateProgressBarCallback(int total, int count)
        {
            m_progressBar.value = (float)count / (float)total;
        }
        private void PlayFadeoutAnimationCallback()
        {
            m_animator.SetTrigger(m_triggerParameter);
        }
    }
}