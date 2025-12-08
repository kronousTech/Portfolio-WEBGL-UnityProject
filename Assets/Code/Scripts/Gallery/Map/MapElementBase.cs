using UnityEditor;
using UnityEngine;

namespace KronosTech.Gallery.Map
{
    public class MapElementBase : MonoBehaviour
    {
        [Header("Map Element References")]
        [SerializeField] private Sprite m_mapImage;

        private int m_ID;
        private MapElementsBuilder m_displayer;

        protected virtual void Awake()
        {
            m_ID = GUID.Generate().GetHashCode();
            m_displayer = FindFirstObjectByType<MapElementsBuilder>(FindObjectsInactive.Include);
        }

        protected void PlaceMapElement()
        {
            m_displayer.DisplayElement(m_ID, m_mapImage, transform);
        }
    }
}