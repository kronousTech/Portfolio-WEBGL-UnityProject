using System;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Gallery.Map
{
    public static class MapMediator
    {
        private static readonly Dictionary<int, MapElementSourceData> s_sources = new();

        public static event Action<MapElementSourceData> OnAdded;
        public static event Action<MapElementSourceData> OnUpdate;
        public static event Action<MapElementSourceData> OnRemoved;

        public static void UpdateSourcePosition(MapElementSourceData data)
        {
            if (!s_sources.ContainsKey(data.ID))
            {
                s_sources.Add(data.ID, data);

                OnAdded?.Invoke(data);
            }
            else
            {
                OnUpdate?.Invoke(data);
            }
        }
        public static void UnRegisterSource(MapElementSourceData data)
        {
            if (!s_sources.ContainsKey(data.ID))
            {
                Debug.LogError($"{nameof(MapMediator)}.cs: " +
                    $"Trying to remove a element that is not present on the dictionary");

                return;
            }

            s_sources.Remove(data.ID);

            OnRemoved?.Invoke(data);
        }
    }
}