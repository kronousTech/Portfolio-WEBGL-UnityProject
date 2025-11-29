using KronosTech.ObjectPooling;
using System;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class PlaceableCorridor : GalleryPoolObjectBase , IPlaceablePieceBase
    {
        [Header("References")]
        [SerializeField] private GalleryTileExit _exit;
        public GalleryTileExit GetExit => _exit;

        public event Action OnPlacement;

        #region IPlaceablePieceBase
        public void Place(GalleryTileExit exit)
        {
            transform.SetPositionAndRotation(exit.Position, exit.Rotation);

            OnPlacement?.Invoke();
        }
        #endregion
    }
}