using KronosTech.Gallery.Rooms;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace KronosTech.Gallery.Generation.TagSelection
{
    public class TagToggle : MonoBehaviour
    {
        private Toggle m_toggle;
        private RoomTagFlags m_selectedTag;

        public event Action<string> OnInitialized;

        private void Awake()
        {
            m_toggle = GetComponent<Toggle>();
        }

        public void Initialize(string tag)
        {
            m_selectedTag = System.Enum.Parse<RoomTagFlags>(tag);

            OnInitialized?.Invoke(tag);
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