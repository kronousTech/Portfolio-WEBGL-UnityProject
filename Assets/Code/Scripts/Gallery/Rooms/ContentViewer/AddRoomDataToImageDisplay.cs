using KronosTech.Gallery.Rooms;
using UnityEngine;

namespace KronosTech.Gallery.Rooms.ContentViewer
{
    public class AddRoomDataToImageDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ContentViewerImages m_imagesDisplay;
        [SerializeField] private ContentViewerVideos m_videosDisplay;
        [SerializeField] private DataRepositoryRoomData m_repository;

        private void OnEnable()
        {
            m_repository.OnDataSet += SetDataToDisplaysCallback;
        }
        private void OnDisable()
        {
            m_repository.OnDataSet -= SetDataToDisplaysCallback;
        }

        private void SetDataToDisplaysCallback(RoomData data)
        {
            m_imagesDisplay.SetData(data.Images);
            m_videosDisplay?.SetData(data.Videos);
        }
    }
}