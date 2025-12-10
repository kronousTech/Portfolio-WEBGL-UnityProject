using KronosTech.Gallery.Generation.Placeables;

namespace KronosTech.Gallery.Map
{
    public class MapElementRoom : MapElementSource
    {
        private PlaceableRoom m_room;

        private void OnEnable()
        {
            m_room.OnPlacement += UpdateMapPositionCallback;
            m_room.OnVisibilityChange += UpdateMapPositionCallback;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            m_room.OnPlacement -= UpdateMapPositionCallback;
            m_room.OnVisibilityChange -= UpdateMapPositionCallback;
        }
        protected override void Awake()
        {
            base.Awake();

            m_room = GetComponent<PlaceableRoom>();
        }

        private void UpdateMapPositionCallback()
        {
            UpdateMapPosition();
        }
        private void UpdateMapPositionCallback(bool value)
        {
            if(!value)
            {
                RemoveSource();
            }
        }
    }
}