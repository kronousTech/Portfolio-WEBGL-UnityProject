using KronosTech.ObjectPooling;
using System;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class PlaceableWall : GalleryPoolObjectBase, IPlaceablePieceBase
    {
        [Header("References")]
        [SerializeField] private GameObject _line;

        public event Action OnPlacement;

        #region IPlaceablePieceBase
        public void Place(GalleryTileExit exit)
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