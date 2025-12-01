using KronosTech.Gallery.Rooms;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Generation.TagSelection
{
    public class TagToggle : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI m_tagText;

        private Toggle m_toggle;
        private RoomTagFlags m_selectedTag;

        private void Awake()
        {
            m_toggle = GetComponent<Toggle>();
        }

        public void Initialize(string tag)
        {
            m_tagText.text = tag;
            m_selectedTag = System.Enum.Parse<RoomTagFlags>(tag);
        }
        public bool GetTag(out RoomTagFlags tag)
        {
            tag = m_selectedTag;

            return m_toggle.isOn;
        }
        public void SetInteractability(bool interactable)
        {
            m_toggle.interactable = interactable;
        }
    }
}