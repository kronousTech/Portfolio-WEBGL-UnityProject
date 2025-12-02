using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Rooms.ContentViewer
{
    public class PresenterContentViewerVideosVolumeIcon : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Sprite m_noSoundIcon;
        [SerializeField] private Sprite m_averageSoundIcon;
        [SerializeField] private Sprite m_fullSoundIcon;
        [Header("References")]
        [SerializeField] private Scrollbar m_scrollbar;
        [SerializeField] private Image m_icon;

        private void OnEnable()
        {
            m_scrollbar.onValueChanged.AddListener(SetVolumeBasedOnSliderCallback);
        }
        private void OnDisable()
        {
            m_scrollbar.onValueChanged.RemoveListener(SetVolumeBasedOnSliderCallback);
        }
        private void SetVolumeBasedOnSliderCallback(float value)
        {
            m_icon.sprite = value switch
            {
                0 => m_noSoundIcon,
                1 => m_fullSoundIcon,
                _ => m_averageSoundIcon,
            };
        }
    }
}