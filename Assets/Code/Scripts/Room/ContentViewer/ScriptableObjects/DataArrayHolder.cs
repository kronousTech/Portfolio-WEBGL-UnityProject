using System;
using UnityEngine;

namespace KronosTech.Room.ContentViewer
{
    [Serializable]
    public class DataArrayHolder<T> : ScriptableObject
    {
        [SerializeField] private T[] m_data;

        public T[] Data
        {
            get { return m_data; }
        }
    }
}
