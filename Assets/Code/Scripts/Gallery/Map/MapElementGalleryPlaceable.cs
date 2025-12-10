using KronosTech.Gallery.Generation.Placeables;

namespace KronosTech.Gallery.Map
{
    public class MapElementGalleryPlaceable : MapElementSource
    {
        private IPlaceablePieceBase m_placeable;

        private void OnEnable()
        {
            m_placeable.OnPlacement += UpdateMapPositionCallback;
        }
        protected override void OnDisable()
        {
            base.OnDisable();

            m_placeable.OnPlacement -= UpdateMapPositionCallback;
        }
        protected override void Awake()
        {
            base.Awake();

            m_placeable = GetComponent<IPlaceablePieceBase>();
        }

        private void UpdateMapPositionCallback()
        {
            UpdateMapPosition();
        }
    }
}