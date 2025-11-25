using KronosTech.AssetBundles;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace KronosTech.Room.ContentViewer
{
    public class ContentDisplayVideos : ContentViewerBase<ContentDataUrl>
    {
        [Header("References")]
        [SerializeField] private VideoPlayer m_videoPlayer;
        [SerializeField] private RawImage m_rawImage;
        [SerializeField] private Button m_buttonPlay;
        [SerializeField] private Button m_buttonPause;
        [SerializeField] private Button _buttonRestart;

        private static Action<ContentDisplayVideos> OnVideoStart;

        public event Action<int> OnInitialize;
        public event Action<ContentDataUrl> OnVideoChange;
        public event Action OnPrepare;
        public event Action<double> OnPrepareCompleted;
        public event Action OnPlayInput;
        public event Action OnPlay;
        public event Action OnRestartInput;
        public event Action OnRestart;
        public event Action OnPause;
        public event Action<double> OnPlaying;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_buttonPlay.onClick.AddListener(() => StartCoroutine(PlayCoroutine()));
            m_buttonPause.onClick.AddListener(PauseVideoCallback);
            _buttonRestart.onClick.AddListener(() => StartCoroutine(Restart()));

            m_videoPlayer.prepareCompleted += (source) => PauseVideoOnPreparedCallback();

            OnVideoStart += DisableOtherVideoPlayers;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            m_buttonPlay.onClick.RemoveListener(() => StartCoroutine(PlayCoroutine()));
            m_buttonPause.onClick.RemoveListener(PauseVideoCallback);
            _buttonRestart.onClick.RemoveListener(() => StartCoroutine(Restart()));

            m_videoPlayer.prepareCompleted -= (source) => PauseVideoOnPreparedCallback();

            OnVideoStart -= DisableOtherVideoPlayers;
        }
        private void Start()
        {
            m_videoPlayer.source = VideoSource.Url;

            var renderTexture = new RenderTexture(800, 600, 0);
            m_videoPlayer.targetTexture = renderTexture;
            m_rawImage.texture = renderTexture;
        }
        private void Update()
        {
            if(m_videoPlayer.isPlaying && m_videoPlayer.isPrepared)
            {
                OnPlaying?.Invoke(m_videoPlayer.time);
            }
        }

        private void DisableOtherVideoPlayers(ContentDisplayVideos controller)
        {
            if(controller != this)
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

        private IEnumerator PrepareCoroutine()
        {
            yield return new WaitForSeconds(2.0f);

            if(!m_videoPlayer.isPrepared && m_videoPlayer.enabled)
                m_videoPlayer.Prepare();
        }

        private void SaveVideoClipToDataCallback(AssetBundleLoadEventArgs<VideoClip> args, int index, Action callback)
        {
            //m_loadedVideosCount += 1;

            if (!args.IsSuccessful)
            {
                Debug.LogError($"{nameof(ContentViewerImages)}.cs: " +
                    $"Failed to load image at index {index}.");

                return;
            }

           //Content.Add(new RoomVideoData(m_content.Data[index].Title, args.Asset));
           //
           //if (m_loadedVideosCount >= m_videosToLoadCount)
           //{
           //    Debug.Log(m_loadedVideosCount + "" + m_videosToLoadCount);
           //
           //    OnInitialize?.Invoke(Content.Count);
           //
           //    callback?.Invoke();
           //}
        }

        #region ContentDisplayBase
        //protected override void LoadData(Action callback)
        //{
            
            //
            //var data = m_content.GetData();
            //if (data.Length == 0)
            //{
            //    return;
            //}
            //
            //m_videosToLoadCount = data.Length;
            //m_loadedVideosCount = 0;
            //
            //Content = new();
            //
            //for (int i = 0; i < data.Length; i++)
            //{
            //    var index = i;
            //    var asset = data[i]..Asset;
            //
            //    AssetBundlesRequest.Load<VideoClip>(asset.Bundle, asset.Name, (args) => SaveVideoClipToDataCallback(args, index, callback));
            //}
        //}
        protected override void ShowContent(ContentDataUrl content)
        {
            OnVideoChange?.Invoke(content);
            
            m_videoPlayer.url = content.Data;
            
            if (m_videoPlayer.isActiveAndEnabled)
            {
                m_videoPlayer.Prepare();
            
                StartCoroutine(PrepareCoroutine());
            }
            
            OnPrepare?.Invoke();
        }
        #endregion
    }
}