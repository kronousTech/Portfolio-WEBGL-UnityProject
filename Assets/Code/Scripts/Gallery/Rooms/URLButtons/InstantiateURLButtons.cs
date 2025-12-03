using KronosTech.CustomPackage.Utilities.Extensions;
using UnityEngine;

namespace KronosTech.Gallery.Rooms.URLButtons
{
    public class InstantiateURLButtons : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private LinkButtonDisplay m_buttonPrefab;
        [Header("References")]
        [SerializeField] private DataRepositoryRoomData m_repository;
        [SerializeField] private RectTransform m_parent;

        private void OnEnable()
        {
            m_repository.OnDataSet += InstantiateButtonsCallback;
        }
        private void OnDisable()
        {
            m_repository.OnDataSet -= InstantiateButtonsCallback;
        }

        private void InstantiateButtonsCallback(RoomData roomData)
        {
            m_parent.DestroyChildren();

            if(roomData.Urls == null || roomData.Urls.Length.Equals(0))
            {
                return;
            }

            foreach (var data in roomData.Urls)
            {
                if(data == null)
                {
                    Debug.LogError($"{nameof(InstantiateURLButtons)}.cs: " +
                        $"The url data is null at {roomData.ProjectName}");

                    continue;
                }

                var urlButton = Instantiate(m_buttonPrefab, m_parent);
                urlButton.Setup(data);
            }
        }
    }
}