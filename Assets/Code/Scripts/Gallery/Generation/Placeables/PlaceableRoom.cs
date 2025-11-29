using KronosTech.Data;
using KronosTech.Gallery.Generation.ObjectPooling;
using KronosTech.Gallery.Rooms;
using System;
using UnityEngine;

namespace KronosTech.Gallery.Generation.Placeables
{
    public class PlaceableRoom : PoolObjectGalleryBase, IPlaceablePieceBase
    {
        [Header("References")]
        [SerializeField] private GameObject _holder;
        [SerializeField] private GameObject _line;
        [SerializeField] private DataRepositoryRoomData m_repository;

        public event Action OnPlacement;

        public void Initialize(RoomData data)
        {
            m_repository.SetData(data);
            transform.name = $"Room: {data.GetFullName()}";
        }
        public void SetVisibility(bool state)
        {
            _holder.SetActive(state);
        }

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