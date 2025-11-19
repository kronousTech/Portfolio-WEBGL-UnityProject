using System;
using UnityEngine;

namespace KronosTech.Data
{
    [Serializable]
    public struct InfoCategory
    {
        public InfoCategories category;
        [TextArea(15, 75)] public string info;
    }
}