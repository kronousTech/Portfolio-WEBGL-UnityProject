using UnityEngine;

namespace KronosTech.Gallery.Map
{
    public class MapElementSourceData
    {
        public int ID { get; }
        public Sprite Sprite { get; }
        public Transform Transform { get; }
        public bool Priority { get; }

        public MapElementSourceData(Sprite sprite, Transform worldTransform, bool priority)
        {
            ID = System.Guid.NewGuid().GetHashCode();
            Sprite = sprite;
            Transform = worldTransform;
            Priority = priority;
        }
    }
}