using System;

namespace KronosTech.Gallery.Generation.Placeables
{
    public interface IPlaceablePieceBase
    {
        public event Action OnPlacement;
        public void Place(PlaceableExit exit);
    }
}