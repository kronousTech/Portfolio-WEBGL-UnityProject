using KronosTech.Gallery.Generation.ObjectPooling;
using System;
using UnityEngine;

namespace KronosTech.Gallery.Generation.Placeables
{
    public class PlaceableCorridor : PoolObjectGalleryBase, IPlaceablePieceBase
    {
        [Header("References")]
        [SerializeField] private PlaceableExit _exit;
        public PlaceableExit GetExit => _exit;

        public event Action OnPlacement;

        #region IPlaceablePieceBase
        public void Place(PlaceableExit exit)
        {
            transform.SetPositionAndRotation(exit.Position, exit.Rotation);

            OnPlacement?.Invoke();
        }
        #endregion
    }
}