using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Rooms.ContentViewer
{
    public class ContentViewerVideosButtonsInteractability : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject m_loadingVideoGO;
        [SerializeField] private Button[] m_playButtons;
        [SerializeField] private Button[] m_pauseButtons;
        [SerializeField] private Button m_buttonRestart;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private ContentViewerVideos m_videoDisplay;

        private void OnEnable()
        {
            m_videoDisplay.OnPrepare += SetLoadingStateCallback;
            m_videoDisplay.OnPrepareCompleted += SetReadyToPlayCallback;
            m_videoDisplay.OnPlayInput += SetLoadingStateCallback;
            m_videoDisplay.OnPlay += SetPlayingStateCallback;
            m_videoDisplay.OnPause += SetReadyToPlayCallback;
            m_videoDisplay.OnRestartInput += SetLoadingStateCallback;
            m_videoDisplay.OnRestart += SetReadyToPlayCallback;
        }
        private void OnDisable()
        {
            m_videoDisplay.OnPrepare -= SetLoadingStateCallback;
            m_videoDisplay.OnPrepareCompleted -= SetReadyToPlayCallback;
            m_videoDisplay.OnPlayInput -= SetLoadingStateCallback;
            m_videoDisplay.OnPlay -= SetPlayingStateCallback;
            m_videoDisplay.OnPause -= SetReadyToPlayCallback;
            m_videoDisplay.OnRestartInput -= SetLoadingStateCallback;
            m_videoDisplay.OnRestart -= SetReadyToPlayCallback;
        }
        private void Awake()
        {
            m_videoDisplay = GetComponentInParent<ContentViewerVideos>(true);
        }
        private void SetPlayingStateCallback()
        {
            SetButtonsInteractability(m_playButtons, true);
            SetButtonsInteractability(m_pauseButtons, true);

            m_buttonRestart.interactable = true;
            m_loadingVideoGO.SetActive(false);

            SetButtonsGameObjectActive(m_playButtons, false);
            SetButtonsGameObjectActive(m_pauseButtons, true);
        }

        private void SetLoadingStateCallback()
        {
            SetButtonsInteractability(m_playButtons, false);
            SetButtonsInteractability(m_pauseButtons, false);

            m_buttonRestart.interactable = false;
            m_loadingVideoGO.SetActive(true);

            SetButtonsGameObjectActive(m_playButtons, true);
            SetButtonsGameObjectActive(m_pauseButtons, false);
        }
        private void SetReadyToPlayCallback(double length)
        {
            SetReadyToPlayCallback();
        }
        private void SetReadyToPlayCallback()
        {
            SetButtonsInteractability(m_playButtons, true);
            SetButtonsInteractability(m_pauseButtons, true);

            m_buttonRestart.interactable = true;
            m_loadingVideoGO.SetActive(false);

            SetButtonsGameObjectActive(m_playButtons, true);
            SetButtonsGameObjectActive(m_pauseButtons, false);
        }

        private void SetButtonsInteractability(Button[] buttons, bool interactable)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = interactable;
            }
        }
        private void SetButtonsGameObjectActive(Button[] buttons, bool active)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(active);
            }
        }

    }
}