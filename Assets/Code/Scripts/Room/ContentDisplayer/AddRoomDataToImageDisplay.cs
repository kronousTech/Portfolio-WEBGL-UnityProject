using KronosTech.Data;
using KronosTech.Room.ContentDisplay;
using UnityEngine;

public class AddRoomDataToImageDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ContentDisplayImages m_imagesDisplay;
    [SerializeField] private ContentDisplayVideos m_videosDisplay;
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
        m_videosDisplay?.SetData(data.Images);
    }
}
