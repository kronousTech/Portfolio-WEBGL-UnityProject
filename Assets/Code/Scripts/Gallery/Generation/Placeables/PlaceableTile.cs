using KronosTech.Gallery.Generation.ObjectPooling;
using System;
using UnityEngine;

namespace KronosTech.Gallery.Generation.Placeables
{
    public class PlaceableTile : PoolObjectGalleryBase, IPlaceablePieceBase
    {
        [Header("References")]
        [SerializeField] private Transform _exitsParent;

        private PlaceableExit[] m_exits;

        public int GetExitsCount { get { return m_exits.Length; } private set { } }

        public event Action OnPlacement;

        public void InitializeGallery(int remainingRooms, PlaceableExit exit, Action<PlaceableExit, PlaceableExit[]> exits)
        {
            m_exits = new PlaceableExit[_exitsParent.childCount];

            for (int i = 0; i < _exitsParent.childCount; i++)
            {
                m_exits[i] = _exitsParent.GetChild(i).GetComponent<PlaceableExit>();
            }

            if (remainingRooms > m_exits.Length)
            {

                for (int i = 1; i < m_exits.Length; i++)
                {
                    var currentExit = m_exits[i];
                    var currentDistance = Vector3.Distance(currentExit.Position, Vector3.zero);
                    var x = i - 1;

                    while (x >= 0 && Vector3.Distance(m_exits[x].Position, Vector3.zero) < currentDistance)
                    {
                        m_exits[x + 1] = m_exits[x];
                        x--;
                    }

                    m_exits[x + 1] = currentExit;
                }

                var furthestIndex = m_exits.Length > 2 ? UnityEngine.Random.Range(0, 2) : 0;
                var roomPositions = new PlaceableExit[m_exits.Length - 1];
                var j = 0;

                for (int i = 0; i < m_exits.Length ; i++)
                {
                    if (i != furthestIndex)
                    {
                        roomPositions[j++] = m_exits[i];
                    }
                }

                exits?.Invoke(m_exits[furthestIndex], roomPositions);
            }
            else
            {
                exits?.Invoke(null, m_exits);
            }
        }

        #region IPlaceablePieceBase
        public void Place(PlaceableExit exit)
        {
            transform.SetPositionAndRotation(exit.Position, exit.Rotation);

            OnPlacement?.Invoke();
        }
        #endregion
    }
}