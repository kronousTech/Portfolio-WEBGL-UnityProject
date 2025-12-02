using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

namespace KronosTech.Gallery.Rooms.ContentViewer
{
    public class ContentViewerVideosTimeBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("References")]
        [SerializeField] private VideoPlayer m_videoPlayer;
        [SerializeField] private Scrollbar m_scrollbar;
        [SerializeField] private Image m_fillImage;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private ContentViewerVideos m_videoDisplay;

        private bool m_isInteracting;

        private void OnEnable()
        {
            m_videoDisplay.OnPrepare += () => m_scrollbar.value = 0;
            m_videoDisplay.OnPrepareCompleted += SetPreparedStateCallback;
            m_videoDisplay.OnRestartInput += () => m_scrollbar.value = 0.0f;

            m_scrollbar.onValueChanged.AddListener(SetVideoTimeCallback);
            
            m_videoPlayer.frameReady += UpdateFillbarCallback;
        }
        private void OnDisable()
        {
            m_videoDisplay.OnPrepare -= () => m_scrollbar.value = 0;
            m_videoDisplay.OnPrepareCompleted -= SetPreparedStateCallback;
            m_videoDisplay.OnRestartInput -= () => m_scrollbar.value = 0.0f;

            m_scrollbar.onValueChanged.RemoveListener(SetVideoTimeCallback);

            m_videoPlayer.frameReady -= UpdateFillbarCallback;
        }
        private void Awake()
        {
            m_videoPlayer.sendFrameReadyEvents = true;
            m_videoDisplay = GetComponentInParent<ContentViewerVideos>(true);
        }

        private void UpdateFillbarCallback(VideoPlayer source, long frameIdx)
        {
            if(m_isInteracting)
            {
                return;
            }

            m_fillImage.fillAmount = (float)(m_videoPlayer.time / m_videoPlayer.length);
        }

        private void SetPreparedStateCallback(double length)
        {
            m_scrollbar.SetValueWithoutNotify(0);
            m_fillImage.fillAmount = 0;
        }
        private void SetVideoTimeCallback(float value)
        {
            m_videoPlayer.time = value * (float)m_videoPlayer.length;
            m_fillImage.fillAmount = value;
        }

        #region IPointerDownHandler
        public void OnPointerDown(PointerEventData eventData)
        {
            m_isInteracting = true;
        }
        #endregion
        #region IPointerUpHandler
        public void OnPointerUp(PointerEventData eventData)
        {
            m_isInteracting = false;
        }
        #endregion
    }
}