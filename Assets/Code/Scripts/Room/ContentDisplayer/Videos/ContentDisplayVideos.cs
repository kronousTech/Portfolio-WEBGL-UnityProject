using KronosTech.Data;
using KronosTech.Services;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace KronosTech.Room.ContentDisplay
{
    public class ContentDisplayVideos : ContentDisplayBase<RoomVideoData>
    {
        [Header("References")]
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _buttonPause;
        [SerializeField] private Button _buttonRestart;

        private static Action<ContentDisplayVideos> OnVideoStart;

        public event Action<int> OnInitialize;
        public event Action<int, string> OnVideoChange;
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

            _buttonPlay.onClick.AddListener(() => StartCoroutine(PlayCoroutine()));
            _buttonPause.onClick.AddListener(Pause);
            _buttonRestart.onClick.AddListener(() => StartCoroutine(Restart()));

            _videoPlayer.prepareCompleted += (source) => PreparedCallback();
            _videoPlayer.prepareCompleted += (source) => Pause();

            OnVideoStart += DisableOtherVideoPlayers;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            _buttonPlay.onClick.RemoveListener(() => StartCoroutine(PlayCoroutine()));
            _buttonPause.onClick.RemoveListener(Pause);
            _buttonRestart.onClick.RemoveListener(() => StartCoroutine(Restart()));

            _videoPlayer.prepareCompleted -= (source) => PreparedCallback();
            _videoPlayer.prepareCompleted -= (source) => Pause();

            OnVideoStart -= DisableOtherVideoPlayers;
        }
        private void Update()
        {
            if(_videoPlayer.isPlaying && _videoPlayer.isPrepared)
            {
                OnPlaying?.Invoke(_videoPlayer.time);
            }
        }

        private void DisableOtherVideoPlayers(ContentDisplayVideos controller)
        {
            if(controller != this)
            {
                _videoPlayer.Pause();
            }
        }

        private void Prepare()
        {
            if (Data.Length >= 1)
            {
                Index = 0;
            }
        }
        private void PreparedCallback()
        {
            OnPrepareCompleted?.Invoke(_videoPlayer.length);
        }
        private void Pause()
        {
            _videoPlayer.Pause();

            OnPause?.Invoke();
        }
        private IEnumerator Restart()
        {
            _videoPlayer.Stop();
            _videoPlayer.Play();

            OnRestartInput?.Invoke();

            while (!_videoPlayer.isPlaying)
            {
                yield return null;
            }

            _videoPlayer.Pause();
            _videoPlayer.Prepare();

            OnRestart?.Invoke();
        }
        private IEnumerator PlayCoroutine()
        {
            _videoPlayer.Play();

            while (!_videoPlayer.isPrepared)
            {
                yield return null;
            }

            _videoPlayer.Play();

            OnPlayInput?.Invoke();

            while (!_videoPlayer.isPlaying)
            {
                yield return null;
            }

            OnPlay?.Invoke();
            OnVideoStart?.Invoke(this);
        }

        public void ForcePause()
        {
            Pause();
        }

        private IEnumerator PrepareCoroutine()
        {
            yield return new WaitForSeconds(2.0f);

            if(!_videoPlayer.isPrepared && _videoPlayer.enabled)
                _videoPlayer.Prepare();
        }

        #region ContentDisplayBase
        protected override void LoadData()
        {
            _videoPlayer.source = VideoSource.Url;

            var renderTexture = new RenderTexture(800, 600, 0);
            _videoPlayer.targetTexture = renderTexture;
            _videoPlayer.GetComponent<RawImage>().texture = renderTexture;

            var data = m_repository.Data.Videos;
            if (data.Length == 0)
            {
                return;
            }

            Data = new RoomVideoData[data.Length];

            for (int i = 0; i < Data.Length; i++)
            {
                Data[i] = new RoomVideoData(
                    title: data[i].title,
                    url: ServiceLocator.Instance.GetWebVideosService().LoadVideo(data[i].asset));
            }

            Prepare();
         
            OnInitialize?.Invoke(Data.Length);
        }
        protected override void ShowContent(int index)
        {
            OnVideoChange?.Invoke(index, Data[index].Title);

            _videoPlayer.url = Data[index].Url;

            if (_videoPlayer.isActiveAndEnabled)
            {
                _videoPlayer.Prepare();

                StartCoroutine(PrepareCoroutine());
            }

            OnPrepare?.Invoke();
        }
        #endregion
    }
}