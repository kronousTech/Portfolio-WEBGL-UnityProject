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

        public void Initialize(string name, string tags, UnityAction clickAction)
        {
            m_nameDisplay.text = name;
            m_tagsDisplay.text = tags;

            m_button.onClick.AddListener(clickAction);
        }
    }
}