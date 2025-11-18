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

        public void AddTeleport(Transform location, string name, string tags)
        {
            var newDisplay = Instantiate(m_displayPrefab, m_parent);
            newDisplay.Initialize(name, tags, () => TeleportPlayerCallback(location));

            if(!_locationsDictionary.TryAdd(location, newDisplay.gameObject))
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