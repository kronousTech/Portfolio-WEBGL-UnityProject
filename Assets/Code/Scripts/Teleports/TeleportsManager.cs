using KronosTech.Data;
using KronosTech.Player;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Teleport
{
    public class TeleportsManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform m_parent;
        [SerializeField] private TeleportLocationDisplay m_displayPrefab;
        [SerializeField] private UiDisplay m_teleportPanel;
        [SerializeField] private FirstPersonController m_playerController;

        private readonly Dictionary<Transform, GameObject> _locationsDictionary = new();

        public void AddTeleport(RoomData roomData, Transform teleportLocation)
        {
            var newDisplay = Instantiate(m_displayPrefab, m_parent);
            newDisplay.Initialize(roomData, () => TeleportPlayerCallback(teleportLocation));

            if(!_locationsDictionary.TryAdd(teleportLocation, newDisplay.gameObject))
            {
                Debug.LogError($"{nameof(TeleportsManager)}.cs: " +
                    $"Trying to add the same teleport twice.");
            }
        }
        public void RemoveTeleport(Transform location)
        {
            if(_locationsDictionary.ContainsKey(location) )
            {
                Destroy(_locationsDictionary[location]);
            }

            _locationsDictionary.Remove(location);
        }

        private void TeleportPlayerCallback(Transform location)
        {
            m_playerController.Teleport(location);
            
            m_teleportPanel.ClosePanel();
        }
    }
}