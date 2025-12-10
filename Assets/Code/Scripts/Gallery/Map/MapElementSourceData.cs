using UnityEngine;

namespace KronosTech.Gallery.Map
{
    public class MapElementSourceData
    {
        public int ID { get; }
        public Sprite Sprite { get; }
        public Transform Transform { get; }

        public MapElementSourceData(Sprite sprite, Transform worldTransform)
        {
            ID = System.Guid.NewGuid().GetHashCode();
            Sprite = sprite;
            Transform = worldTransform;
        }
    }
}