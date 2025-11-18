using System;
using UnityEngine;

namespace KronosTech.Data
{
    [Serializable]
    public readonly struct RoomSpriteData
    {
        public string Title { get; }
        public Sprite Sprite { get; }

        public RoomSpriteData(string title, Sprite sprite)
        {
            Title = title;
            Sprite = sprite;
        }
    }
}