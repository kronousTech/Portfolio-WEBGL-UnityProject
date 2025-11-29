using KronosTech.Gallery.Generation.ObjectPooling;
using System;
using UnityEngine;

namespace KronosTech.Gallery.Generation.Placeables
{
    public class PlaceableWall : PoolObjectGalleryBase, IPlaceablePieceBase
    {
        [Header("References")]
        [SerializeField] private GameObject _line;

        public event Action OnPlacement;

        #region IPlaceablePieceBase
        public void Place(PlaceableExit exit)
        {
            transform.SetPositionAndRotation(exit.Position, exit.Rotation);

            if (_line != null)
            {
                _line.SetActive(exit.AddLines);
            }

            OnPlacement?.Invoke();
        }
        #endregion
    }
}