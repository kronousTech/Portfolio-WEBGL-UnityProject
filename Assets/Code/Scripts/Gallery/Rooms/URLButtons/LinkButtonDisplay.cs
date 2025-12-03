using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Rooms.URLButtons
{
    public class LinkButtonDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private OpenURL m_openButton;
        [SerializeField] private Image m_icon;

        public void Setup(RoomClickableLinkData data)
        {
            m_openButton.SetNewURL(data.URL);
            m_icon.sprite = data.Icon;
        }
    }
}