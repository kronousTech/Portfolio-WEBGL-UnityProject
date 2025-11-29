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
        [SerializeField] private DataRepositoryRoomData m_repository;
        [SerializeField] private TextMeshPro m_textDisplay;

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
            m_textDisplay.fontSize = m_textFontSize;
        }

        private void SetTextCallback(RoomData data)
        {
            var fullText = new StringBuilder();

            foreach (var text in data.Texts)
            {
                fullText.AppendLine($"<b><size={m_headersFontSize}>{text.category}</b></size>");
                fullText.AppendLine($"{text.info}\n");
            }

            m_textDisplay.text = fullText.ToString();
        }
    }
}