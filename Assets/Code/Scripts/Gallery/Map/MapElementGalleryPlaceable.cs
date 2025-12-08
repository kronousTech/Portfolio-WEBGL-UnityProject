using KronosTech.Gallery.Generation.Placeables;

namespace KronosTech.Gallery.Map
{
    public class MapElementGalleryPlaceable : MapElementBase
    {
        private IPlaceablePieceBase m_placeable;

        private void OnEnable()
        {
            m_placeable.OnPlacement += PlaceElementCallback;
        }
        private void OnDisable()
        {
            m_placeable.OnPlacement -= PlaceElementCallback;
        }
        protected override void Awake()
        {
            base.Awake();

            m_placeable = GetComponent<IPlaceablePieceBase>();
        }

        private void PlaceElementCallback()
        {
            PlaceMapElement();
        }
    }
}