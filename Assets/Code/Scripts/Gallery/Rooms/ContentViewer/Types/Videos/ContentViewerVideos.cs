using KronosTech.Gallery.Rooms.ContentViewer.Data;
using KronosTech.Gallery.Rooms.Optimization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace KronosTech.Gallery.Rooms.ContentViewer
{
    public class ContentViewerVideos : ContentViewerBase<ContentDataUrl>
    {
        [Header("References")]
        [SerializeField] private OnVisibilityEvents m_visibilityEvents;
        [SerializeField] private VideoPlayer m_videoPlayer;
        [SerializeField] private RawImage m_rawImage;
        [SerializeField] private Button[] m_playButtons;
        [SerializeField] private Button[] m_pauseButtons;
        [SerializeField] private Button m_buttonRestart;

        private static Action<ContentViewerVideos> OnVideoStart;

        public event Action OnPrepare;
        public event Action<double> OnPrepareCompleted;
        public event Action OnPlayInput;
        public event Action OnPlay;
        public event Action OnRestartInput;
        public event Action OnRestart;
        public event Action OnPause;

        protected override void OnEnable()
        {
            base.OnEnable();

            for (int i = 0; i < m_playButtons.Length; i++)
            {
                m_playButtons[i].onClick.AddListener(() => StartCoroutine(PlayCoroutine()));
            }
            for (int i = 0; i < m_pauseButtons.Length; i++)
            {
                m_pauseButtons[i].onClick.AddListener(PauseVideoCallback);
            }
            m_buttonRestart.onClick.AddListener(() => StartCoroutine(Restart()));

            m_videoPlayer.prepareCompleted += (source) => PauseVideoOnPreparedCallback();

            OnVideoStart += DisableOtherVideoPlayersCallback;

            m_visibilityEvents.OnBecameVisibleEvent.AddListener(PrepareVideoCallback);
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            for (int i = 0; i < m_playButtons.Length; i++)
            {
                m_playButtons[i].onClick.RemoveListener(() => StartCoroutine(PlayCoroutine()));
            }
            for (int i = 0; i < m_pauseButtons.Length; i++)
            {
                m_pauseButtons[i].onClick.RemoveListener(PauseVideoCallback);
            }
            m_buttonRestart.onClick.RemoveListener(() => StartCoroutine(Restart()));

            m_videoPlayer.prepareCompleted -= (source) => PauseVideoOnPreparedCallback();

            OnVideoStart -= DisableOtherVideoPlayersCallback;

            m_visibilityEvents.OnBecameVisibleEvent.RemoveListener(PrepareVideoCallback);
        }
        protected override void Awake()
        {
            base.Awake();

            m_videoPlayer.source = VideoSource.Url;

            var renderTexture = new RenderTexture(800, 600, 0);
            m_videoPlayer.targetTexture = renderTexture;
            m_rawImage.texture = renderTexture;
        }
        

        private void PrepareVideoCallback()
        {
            if (!m_videoPlayer.gameObject.activeInHierarchy)
            {
                return;
            }
            if (!m_videoPlayer.isPrepared && m_videoPlayer.enabled)
            {
                m_videoPlayer.Prepare();
            }
        }
        private void DisableOtherVideoPlayersCallback(ContentViewerVideos controller)
        {
            if(controller != this && m_videoPlayer.gameObject.activeInHierarchy)
            {
                m_videoPlayer.Pause();
            }
        }
        private void PauseVideoCallback()
        {
            Pause();
        }
        private void PauseVideoOnPreparedCallback()
        {
            Pause();

            OnPrepareCompleted?.Invoke(m_videoPlayer.length);
        }

        private IEnumerator Restart()
        {
            m_videoPlayer.Stop();
            m_videoPlayer.Play();

            OnRestartInput?.Invoke();

            while (!m_videoPlayer.isPlaying)
            {
                yield return null;
            }

            m_videoPlayer.Pause();
            m_videoPlayer.Prepare();

            OnRestart?.Invoke();
        }
        private IEnumerator PlayCoroutine()
        {
            m_videoPlayer.Play();

            while (!m_videoPlayer.isPrepared)
            {
                yield return null;
            }

            m_videoPlayer.Play();

            OnPlayInput?.Invoke();

            while (!m_videoPlayer.isPlaying)
            {
                yield return null;
            }

            OnPlay?.Invoke();
            OnVideoStart?.Invoke(this);
        }

        public void Pause()
        {
            m_videoPlayer.Pause();

            OnPause?.Invoke();
        }

        #region ContentDisplayBase
        protected override void ShowContent(ContentDataUrl content)
        {
            m_videoPlayer.url = content.Data;
            
            if (m_videoPlayer.isActiveAndEnabled)
            {
                m_videoPlayer.Prepare();
            }
            
            OnPrepare?.Invoke();
        }
        #endregion
    }
}