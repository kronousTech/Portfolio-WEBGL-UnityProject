using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace KronosTech.Room.ContentViewer
{
    public class ContentViewerVideosTimeBar : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private VideoPlayer m_videoPlayer;
        [SerializeField] private Slider m_timebar;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private ContentViewerVideos m_videoDisplay;

        private void OnEnable()
        {
            m_videoDisplay.OnPrepare += () => m_timebar.value = 0;
            m_videoDisplay.OnPrepareCompleted += SetPreparedStateCallback;
            m_videoDisplay.OnRestartInput += () => m_timebar.value = 0.0f;

            m_timebar.onValueChanged.AddListener(SetVideoTimeCallback);
        }
        private void OnDisable()
        {
            m_videoDisplay.OnPrepare -= () => m_timebar.value = 0;
            m_videoDisplay.OnPrepareCompleted -= SetPreparedStateCallback;
            m_videoDisplay.OnRestartInput -= () => m_timebar.value = 0.0f;

            m_timebar.onValueChanged.RemoveListener(SetVideoTimeCallback);
        }
        private void Awake()
        {
            m_videoDisplay = GetComponentInParent<ContentViewerVideos>(true);
        }
        private void Update()
        {
            if (m_videoPlayer.isPlaying && m_videoPlayer.isPrepared)
            {
                m_timebar.SetValueWithoutNotify((float)m_videoPlayer.time);
            }
        }

        private void SetPreparedStateCallback(double length)
        {
            m_timebar.maxValue = (float)length;
            m_timebar.SetValueWithoutNotify(0);
        }

        private void SetVideoTimeCallback(float arg0)
        {
            m_videoPlayer.time = arg0;
        }
    }
}