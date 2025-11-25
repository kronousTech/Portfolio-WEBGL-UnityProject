using KronosTech.Room.ContentViewer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.ShowroomGeneration.Room.Videoplayer
{
    public class RoomVideoplayerUIBehavior : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup _buttonsLayout;
        [SerializeField] private ContentDisplayVideos m_videoDisplay;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _indexDisplay;
        [SerializeField] private GameObject _loadingVideoGO;
        [SerializeField] private Slider _timebar;
        [SerializeField] private Button _buttonPlay;
        [SerializeField] private Button _buttonPause;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private Button _buttonNext;
        [SerializeField] private Button _buttonPrev;

        private void OnEnable()
        {
            m_videoDisplay.OnInitialize += (count) => _indexDisplay.gameObject.SetActive(count > 1);
            m_videoDisplay.OnInitialize += (count) => _buttonNext.gameObject.SetActive(count > 1);
            m_videoDisplay.OnInitialize += (count) => _buttonPrev.gameObject.SetActive(count > 1);

            m_videoDisplay.OnPrepare += () => _buttonPlay.interactable = false;
            m_videoDisplay.OnPrepare += () => _buttonPause.interactable = false;
            m_videoDisplay.OnPrepare += () => _buttonRestart.interactable = false;
            m_videoDisplay.OnPrepare += () => _loadingVideoGO.SetActive(true);
            m_videoDisplay.OnPrepare += () => _buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnPrepare += () => _buttonPause.gameObject.SetActive(false);
            m_videoDisplay.OnPrepare += () => _timebar.value = 0;

            m_videoDisplay.OnPrepareCompleted += (length) => _buttonPlay.interactable = true;
            m_videoDisplay.OnPrepareCompleted += (length) => _buttonPause.interactable = true;
            m_videoDisplay.OnPrepareCompleted += (length) => _buttonRestart.interactable = true;
            m_videoDisplay.OnPrepareCompleted += (length) => _loadingVideoGO.SetActive(false);
            m_videoDisplay.OnPrepareCompleted += (length) => _timebar.maxValue = (float)length;

            m_videoDisplay.OnPlaying += (value) => _timebar.value = (float)value;

            m_videoDisplay.OnPlayInput += () => _buttonPlay.interactable = false;
            m_videoDisplay.OnPlayInput += () => _buttonPause.interactable = false;
            m_videoDisplay.OnPlayInput += () => _buttonRestart.interactable = false;
            m_videoDisplay.OnPlay += () => _buttonPlay.interactable = true;
            m_videoDisplay.OnPlay += () => _buttonPause.interactable = true;
            m_videoDisplay.OnPlay += () => _buttonRestart.interactable = true;
            m_videoDisplay.OnPlay += () => _buttonPlay.gameObject.SetActive(false);
            m_videoDisplay.OnPlay += () => _buttonPause.gameObject.SetActive(true);

            m_videoDisplay.OnPause += () => _buttonPlay.interactable = true;
            m_videoDisplay.OnPause += () => _buttonPause.interactable = true;
            m_videoDisplay.OnPause += () => _buttonRestart.interactable = true;
            m_videoDisplay.OnPause += () => _buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnPause += () => _buttonPause.gameObject.SetActive(false);

            m_videoDisplay.OnRestartInput += () => _buttonPlay.interactable = false;
            m_videoDisplay.OnRestartInput += () => _buttonPause.interactable = false;
            m_videoDisplay.OnRestartInput += () => _buttonRestart.interactable = false;
            m_videoDisplay.OnRestartInput += () => _timebar.value = 0.0f;

            m_videoDisplay.OnRestart += () => _buttonPlay.interactable = true;
            m_videoDisplay.OnRestart += () => _buttonPause.interactable = true;
            m_videoDisplay.OnRestart += () => _buttonRestart.interactable = true;
            m_videoDisplay.OnRestart += () => _buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnRestart += () => _buttonPause.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            m_videoDisplay.OnInitialize -= (count) => _indexDisplay.gameObject.SetActive(count > 1);
            m_videoDisplay.OnInitialize -= (count) => _buttonNext.gameObject.SetActive(count > 1);
            m_videoDisplay.OnInitialize -= (count) => _buttonPrev.gameObject.SetActive(count > 1);

            m_videoDisplay.OnPrepare -= () => _buttonPlay.interactable = false;
            m_videoDisplay.OnPrepare -= () => _buttonPause.interactable = false;
            m_videoDisplay.OnPrepare -= () => _buttonRestart.interactable = false;
            m_videoDisplay.OnPrepare -= () => _loadingVideoGO.SetActive(true);
            m_videoDisplay.OnPrepare -= () => _buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnPrepare -= () => _buttonPause.gameObject.SetActive(false);
            m_videoDisplay.OnPrepare -= () => _timebar.value = 0;

            m_videoDisplay.OnPrepareCompleted -= (length) => _buttonPlay.interactable = true;
            m_videoDisplay.OnPrepareCompleted -= (length) => _buttonPause.interactable = true;
            m_videoDisplay.OnPrepareCompleted -= (length) => _buttonRestart.interactable = true;
            m_videoDisplay.OnPrepareCompleted -= (length) => _loadingVideoGO.SetActive(false);
            m_videoDisplay.OnPrepareCompleted -= (length) => _timebar.maxValue = (float)length;

            m_videoDisplay.OnPlaying -= (value) => _timebar.value = (float)value;

            m_videoDisplay.OnPlayInput -= () => _buttonPlay.interactable = false;
            m_videoDisplay.OnPlayInput -= () => _buttonPause.interactable = false;
            m_videoDisplay.OnPlayInput -= () => _buttonRestart.interactable = false;

            m_videoDisplay.OnPlay -= () => _buttonPlay.interactable = true;
            m_videoDisplay.OnPlay -= () => _buttonPause.interactable = true;
            m_videoDisplay.OnPlay -= () => _buttonRestart.interactable = true;
            m_videoDisplay.OnPlay -= () => _buttonPlay.gameObject.SetActive(false);
            m_videoDisplay.OnPlay -= () => _buttonPause.gameObject.SetActive(true);

            m_videoDisplay.OnPause -= () => _buttonPlay.interactable = true;
            m_videoDisplay.OnPause -= () => _buttonPause.interactable = true;
            m_videoDisplay.OnPause -= () => _buttonRestart.interactable = true;
            m_videoDisplay.OnPause -= () => _buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnPause -= () => _buttonPause.gameObject.SetActive(false);

            m_videoDisplay.OnRestartInput -= () => _buttonPlay.interactable = false;
            m_videoDisplay.OnRestartInput -= () => _buttonPause.interactable = false;
            m_videoDisplay.OnRestartInput -= () => _buttonRestart.interactable = false;
            m_videoDisplay.OnRestartInput -= () => _timebar.value = 0.0f;

            m_videoDisplay.OnRestart -= () => _buttonPlay.interactable = true;
            m_videoDisplay.OnRestart -= () => _buttonPause.interactable = true;
            m_videoDisplay.OnRestart -= () => _buttonRestart.interactable = true;
            m_videoDisplay.OnRestart -= () => _buttonPlay.gameObject.SetActive(true);
            m_videoDisplay.OnRestart -= () => _buttonPause.gameObject.SetActive(false);
        }
    }
}