using System;

namespace KronosTech.ShowroomGeneration
{
    public interface IPlaceablePieceBase
    {
        public event Action OnPlacement;
        public void Place(GalleryTileExit exit);
    }
}