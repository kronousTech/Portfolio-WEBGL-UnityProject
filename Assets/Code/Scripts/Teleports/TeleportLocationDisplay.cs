using KronosTech.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KronosTech.Teleport
{
    public class TeleportLocationDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI m_nameDisplay;
        [SerializeField] private TextMeshProUGUI m_tagsDisplay;
        [SerializeField] private Button m_button;

        public void Initialize(RoomData roomData, UnityAction clickAction)
        {
            m_nameDisplay.text = roomData.GetFullName();
            m_tagsDisplay.text = roomData.Tags.ToString();

            m_button.onClick.AddListener(clickAction);
        }
    }
}