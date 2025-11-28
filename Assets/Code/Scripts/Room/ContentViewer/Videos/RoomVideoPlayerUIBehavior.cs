using KronosTech.Room.ContentViewer;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Room.ContentViewer
{
    public class RoomVideoPlayerUIBehavior : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ContentViewerVideos m_videoDisplay;
        [SerializeField] private GameObject m_loadingVideoGO;
        [SerializeField] private Button m_buttonPlay;
        [SerializeField] private Button m_buttonPause;
        [SerializeField] private Button m_buttonRestart;

        private void OnEnable()
        {
            m_videoDisplay.OnPrepare += () => m_buttonPlay.interactable = false;
            m_videoDisplay.OnPrepare += () => m_buttonPause.interactable = false;
            m_videoDisplay.OnPrepare += () => m_buttonRestart.interactable = false;
            m_videoDisplay.OnPrepare += () => m_loadingVideoGO.SetActive(true);
            m_videoDisplay.OnPrepare += () => m_buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnPrepare += () => m_buttonPause.gameObject.SetActive(false);

            m_videoDisplay.OnPrepareCompleted += (length) => m_buttonPlay.interactable = true;
            m_videoDisplay.OnPrepareCompleted += (length) => m_buttonPause.interactable = true;
            m_videoDisplay.OnPrepareCompleted += (length) => m_buttonRestart.interactable = true;
            m_videoDisplay.OnPrepareCompleted += (length) => m_loadingVideoGO.SetActive(false);

            m_videoDisplay.OnPlayInput += () => m_buttonPlay.interactable = false;
            m_videoDisplay.OnPlayInput += () => m_buttonPause.interactable = false;
            m_videoDisplay.OnPlayInput += () => m_buttonRestart.interactable = false;
            m_videoDisplay.OnPlay += () => m_buttonPlay.interactable = true;
            m_videoDisplay.OnPlay += () => m_buttonPause.interactable = true;
            m_videoDisplay.OnPlay += () => m_buttonRestart.interactable = true;
            m_videoDisplay.OnPlay += () => m_buttonPlay.gameObject.SetActive(false);
            m_videoDisplay.OnPlay += () => m_buttonPause.gameObject.SetActive(true);

            m_videoDisplay.OnPause += () => m_buttonPlay.interactable = true;
            m_videoDisplay.OnPause += () => m_buttonPause.interactable = true;
            m_videoDisplay.OnPause += () => m_buttonRestart.interactable = true;
            m_videoDisplay.OnPause += () => m_buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnPause += () => m_buttonPause.gameObject.SetActive(false);

            m_videoDisplay.OnRestartInput += () => m_buttonPlay.interactable = false;
            m_videoDisplay.OnRestartInput += () => m_buttonPause.interactable = false;
            m_videoDisplay.OnRestartInput += () => m_buttonRestart.interactable = false;

            m_videoDisplay.OnRestart += () => m_buttonPlay.interactable = true;
            m_videoDisplay.OnRestart += () => m_buttonPause.interactable = true;
            m_videoDisplay.OnRestart += () => m_buttonRestart.interactable = true;
            m_videoDisplay.OnRestart += () => m_buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnRestart += () => m_buttonPause.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            m_videoDisplay.OnPrepare -= () => m_buttonPlay.interactable = false;
            m_videoDisplay.OnPrepare -= () => m_buttonPause.interactable = false;
            m_videoDisplay.OnPrepare -= () => m_buttonRestart.interactable = false;
            m_videoDisplay.OnPrepare -= () => m_loadingVideoGO.SetActive(true);
            m_videoDisplay.OnPrepare -= () => m_buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnPrepare -= () => m_buttonPause.gameObject.SetActive(false);

            m_videoDisplay.OnPrepareCompleted -= (length) => m_buttonPlay.interactable = true;
            m_videoDisplay.OnPrepareCompleted -= (length) => m_buttonPause.interactable = true;
            m_videoDisplay.OnPrepareCompleted -= (length) => m_buttonRestart.interactable = true;
            m_videoDisplay.OnPrepareCompleted -= (length) => m_loadingVideoGO.SetActive(false);

            m_videoDisplay.OnPlayInput -= () => m_buttonPlay.interactable = false;
            m_videoDisplay.OnPlayInput -= () => m_buttonPause.interactable = false;
            m_videoDisplay.OnPlayInput -= () => m_buttonRestart.interactable = false;

            m_videoDisplay.OnPlay -= () => m_buttonPlay.interactable = true;
            m_videoDisplay.OnPlay -= () => m_buttonPause.interactable = true;
            m_videoDisplay.OnPlay -= () => m_buttonRestart.interactable = true;
            m_videoDisplay.OnPlay -= () => m_buttonPlay.gameObject.SetActive(false);
            m_videoDisplay.OnPlay -= () => m_buttonPause.gameObject.SetActive(true);

            m_videoDisplay.OnPause -= () => m_buttonPlay.interactable = true;
            m_videoDisplay.OnPause -= () => m_buttonPause.interactable = true;
            m_videoDisplay.OnPause -= () => m_buttonRestart.interactable = true;
            m_videoDisplay.OnPause -= () => m_buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnPause -= () => m_buttonPause.gameObject.SetActive(false);

            m_videoDisplay.OnRestartInput -= () => m_buttonPlay.interactable = false;
            m_videoDisplay.OnRestartInput -= () => m_buttonPause.interactable = false;
            m_videoDisplay.OnRestartInput -= () => m_buttonRestart.interactable = false;

            m_videoDisplay.OnRestart -= () => m_buttonPlay.interactable = true;
            m_videoDisplay.OnRestart -= () => m_buttonPause.interactable = true;
            m_videoDisplay.OnRestart -= () => m_buttonRestart.interactable = true;
            m_videoDisplay.OnRestart -= () => m_buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnRestart -= () => m_buttonPause.gameObject.SetActive(false);
        }

    }
}