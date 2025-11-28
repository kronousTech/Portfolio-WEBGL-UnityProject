using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static UnityEngine.Rendering.DebugUI;

namespace KronosTech.Room.ContentViewer
{
    public class ContentViewerVideosTimeBar : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ContentViewerVideos m_videoDisplay;
        [SerializeField] private VideoPlayer m_videoPlayer;
        [SerializeField] private Slider m_timebar;

        private void OnEnable()
        {
            m_videoDisplay.OnPrepare += () => m_timebar.value = 0;
            m_videoDisplay.OnPrepareCompleted += (length) => m_timebar.maxValue = (float)length;
            m_videoDisplay.OnRestartInput += () => m_timebar.value = 0.0f;

            m_timebar.onValueChanged.AddListener(SetVideoTimeCallback);
        }
        private void OnDisable()
        {
            m_videoDisplay.OnPrepare -= () => m_timebar.value = 0;
            m_videoDisplay.OnPrepareCompleted -= (length) => m_timebar.maxValue = (float)length;
            m_videoDisplay.OnRestartInput -= () => m_timebar.value = 0.0f;

            m_timebar.onValueChanged.RemoveListener(SetVideoTimeCallback);
        }
        private void Update()
        {
            if (m_videoPlayer.isPlaying && m_videoPlayer.isPrepared)
            {
                m_timebar.SetValueWithoutNotify((float)m_videoPlayer.time);
            }
        }
        private void SetVideoTimeCallback(float arg0)
        {
            m_videoPlayer.time = arg0;
        }
    }
}