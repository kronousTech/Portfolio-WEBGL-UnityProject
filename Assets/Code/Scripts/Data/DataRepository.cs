using System;
using UnityEngine;

namespace KronosTech.Data
{
    public class DataRepository<TDataType> : MonoBehaviour  where TDataType : class
    {
        public TDataType Data { get; private set; }

        public event Action<TDataType> OnDataSet;

        public void SetData(TDataType value)
        {
            Data = value;

            OnDataSet?.Invoke(Data);
        }
    }
}