using NaughtyAttributes;
using System.Text;
using TMPro;
using UnityEngine;

namespace KronosTech.Gallery.Rooms.Presenters
{
    public class PresenterRoomText : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int m_headersFontSize = 90;
        [SerializeField] private int m_textFontSize = 70;
        [Header("References")]
        [SerializeField] private TextMeshPro m_textDisplay;
        [Header("Debug View")]
        [SerializeField, ReadOnly] private DataRepositoryRoomData m_repository;
        
        private void OnEnable()
        {
            m_repository.OnDataSet += SetTextCallback;
        }
        private void OnDisable()
        {
            m_repository.OnDataSet -= SetTextCallback;
        }
        private void Awake()
        {
            m_repository = GetComponentInParent<DataRepositoryRoomData>();
            m_textDisplay.fontSize = m_textFontSize;
        }


        private void SetTextCallback(RoomData data)
        {
            var fullText = new StringBuilder();

            foreach (var text in data.Texts)
            {
                fullText.AppendLine($"<b><size={m_headersFontSize}>{GetCategoryText(text.category)}</b></size>");
                fullText.AppendLine($"{text.info.TrimEnd('\r', '\n')}\n");
            }

            m_textDisplay.text = fullText.ToString();
        }
        private string GetCategoryText(TextSectionType sectionType)
        {
            switch (sectionType)
            {
                case TextSectionType.WhatIDid:
                    return "What I Did";
            }
            
            return sectionType.ToString();
        }
    }
}