using System;
using UnityEngine;

namespace KronosTech.Gallery.Rooms
{
    [Serializable]
    public struct InfoCategoryData
    {
        public TextSectionType category;
        [TextArea(15, 75)] public string info;
    }
}