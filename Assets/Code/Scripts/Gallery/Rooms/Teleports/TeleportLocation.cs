using UnityEngine;

namespace KronosTech.Gallery.Rooms.Teleportation
{
    public class TeleportLocation : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected RoomData m_roomData;
        [SerializeField] private Transform m_transformLocation;

        public RoomData Data
        {
            get { return m_roomData; }
        }

        private TeleportsManager _teleportsManager;

        protected virtual void Awake()
        {
            _teleportsManager = FindFirstObjectByType<TeleportsManager>(FindObjectsInactive.Include);
        }
        private void OnEnable()
        {
            if(m_roomData == null)
            { 
                return; 
            }

            _teleportsManager.AddTeleport(m_roomData, m_transformLocation);
        }
        private void OnDisable()
        {
            _teleportsManager.RemoveTeleport(transform);
        }
    }
}