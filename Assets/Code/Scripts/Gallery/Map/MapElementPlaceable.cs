using KronosTech.Gallery.Generation.Placeables;

namespace KronosTech.Gallery.Map
{
    public class MapElementPlaceable : MapElementSource
    {
        private IPlaceablePieceBase m_placeable;

        private void OnEnable()
        {
            UpdateMapPosition();

            m_placeable.OnPlacement += UpdateMapPositionCallback;
        }
        private void OnDisable()
        {
            m_placeable.OnPlacement -= UpdateMapPositionCallback;

            RemoveSource();
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