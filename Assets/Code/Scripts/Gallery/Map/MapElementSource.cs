using UnityEngine;

namespace KronosTech.Gallery.Map
{
    public class MapElementSource : MonoBehaviour
    {
        [Header("Map Element References")]
        [SerializeField] private Sprite m_mapImage;

        private MapElementSourceData m_data;

        protected virtual void Awake()
        {
            m_data = new MapElementSourceData(m_mapImage, transform);
        }
        private void OnDestroy()
        {
            RemoveSource();
        }

        protected void UpdateMapPosition()
        {
            MapMediator.UpdateSourcePosition(m_data);
        }
        protected void RemoveSource()
        {
            MapMediator.UnRegisterSource(m_data);
        }
    }
}