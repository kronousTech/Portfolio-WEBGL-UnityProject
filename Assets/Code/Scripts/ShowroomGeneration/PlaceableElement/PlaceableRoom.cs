using KronosTech.Data;
using KronosTech.ObjectPooling;
using System;
using UnityEngine;

namespace KronosTech.ShowroomGeneration
{
    public class PlaceableRoom : GalleryPoolObjectBase, IPlaceablePieceBase
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