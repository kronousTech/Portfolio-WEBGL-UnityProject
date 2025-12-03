using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using KronosTech.UI.UIPanels;

namespace KronosTech.Gallery.Rooms.Teleportation
{
    public class TeleportsManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform m_parent;
        [SerializeField] private TeleportLocationDisplay m_displayPrefab;
        [SerializeField] private UIPanel m_teleportPanel;

        private readonly Dictionary<Transform, GameObject> m_locationsDictionary = new();

        private IEnumerable<ITeleportable> m_teleportableElements;

        private void Awake()
        {
            m_teleportableElements = 
                FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .OfType<ITeleportable>();
        }

        public void AddTeleport(RoomData roomData, Transform teleportLocation)
        {
            var newDisplay = Instantiate(m_displayPrefab, m_parent);
            newDisplay.Initialize(roomData, () => TeleportPlayerCallback(teleportLocation));

            if(!m_locationsDictionary.TryAdd(teleportLocation, newDisplay.gameObject))
            {
                Debug.LogError($"{nameof(TeleportsManager)}.cs: " +
                    $"Trying to add the same teleport twice.");
            }
        }
        public void RemoveTeleport(Transform location)
        {
            if(m_locationsDictionary.ContainsKey(location) )
            {
                Destroy(m_locationsDictionary[location]);
            }

            m_locationsDictionary.Remove(location);
        }

        private void TeleportPlayerCallback(Transform location)
        {
            foreach (var item in m_teleportableElements)
            {
                item.Teleport(location);
            }
            
            m_teleportPanel.ClosePanel();
        }
    }
}