using TMPro;
using UnityEngine;

namespace KronosTech.Gallery.Rooms.Presenters
{
    public class PresenterRoomInfoPanel : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private DataRepositoryRoomData m_repository;
        [SerializeField] private TextMeshPro m_title;
        [SerializeField] private TextMeshPro m_tags;

        private void OnEnable()
        {
            m_repository.OnDataSet += SetTextsCallback;
        }
        private void OnDisable()
        {
            m_repository.OnDataSet -= SetTextsCallback;
        }

        private void SetTextsCallback(RoomData data)
        {
            m_title.text = $"{data.ClientName}-{data.ProjectName}";
            m_tags.text = "[" + data.Tags.ToString() + "]";
        }
    }
}
