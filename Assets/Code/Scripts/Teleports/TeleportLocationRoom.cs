using KronosTech.Data;
using UnityEngine;

namespace KronosTech.Teleport
{
    public class TeleportLocationRoom : TeleportLocation
    {
        [Header("References")]
        [SerializeField] private DataRepositoryRoomData m_repository;

        protected override void Awake()
        {
            base.Awake();

            m_repository.OnDataSet += SetDataCallback;
        }
        private void OnDestroy()
        {
            m_repository.OnDataSet -= SetDataCallback;
        }

        private void SetDataCallback(RoomData data)
        {
            m_roomData = data;
        }
    }
}
