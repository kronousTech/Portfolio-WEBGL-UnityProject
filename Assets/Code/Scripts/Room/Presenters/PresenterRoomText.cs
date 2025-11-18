using KronosTech.Data;
using TMPro;
using UnityEngine;

namespace KronosTech.Room
{
    public class PresenterRoomText : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DataRepositoryRoomData m_repository;
        [SerializeField] private TextMeshPro m_display;

        private void OnEnable()
        {
            m_repository.OnDataSet += SetTextCallback;
        }
        private void OnDisable()
        {
            m_repository.OnDataSet -= SetTextCallback;
        }

        private void SetTextCallback(RoomData data)
        {
            var fullText = string.Empty;

            foreach (var text in data.Texts)
            {
                fullText += "<b><size=90>" + text.category.ToString() + "</b></size>\n";
                fullText += text.info + "\n\n";
            }

            m_display.text = fullText;
        }
    }
}