using KronosTech.CustomPackage.Utilities.Extensions;
using NaughtyAttributes;
using TMPro;
using UnityEngine;


namespace KronosTech.AssetBundles
{
    public class PresenterRandomLoadingMessages : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private AssetBundleDownloadAll m_downloader;
        [SerializeField] private StringArrayData m_messagesData;
        [SerializeField] private TextMeshProUGUI m_messageDisplay;
        [SerializeField] private Animator m_animator;

        private void OnEnable()
        {
            m_downloader.OnDownloadStart += StartMessagesLoopCallback;
            m_downloader.OnAllBundlesDownloaded += EndMessagesLoopCallback;
        }
        private void OnDisable()
        {
            m_downloader.OnDownloadStart -= StartMessagesLoopCallback;
            m_downloader.OnAllBundlesDownloaded -= EndMessagesLoopCallback;
        }
        private void Start()
        {
            ChangeToRandomMessage();
        }
        private void StartMessagesLoopCallback()
        {
            m_animator.enabled = true;
        }
        private void EndMessagesLoopCallback()
        {
            m_animator.enabled = false;
        }

        private void ChangeToRandomMessage()
        {
            m_messageDisplay.text = m_messagesData.Strings.GetRandomElement();
        }

        [Button("Set random message")]
        private void ChangeMessageAnimationEvent()
        {
            ChangeToRandomMessage();
        }
    }
}