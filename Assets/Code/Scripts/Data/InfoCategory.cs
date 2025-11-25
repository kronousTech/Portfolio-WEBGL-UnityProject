using System;
using UnityEngine;

namespace KronosTech.Data
{
    [Serializable]
    public struct InfoCategory
    {
        public CategoryType category;
        [TextArea(15, 75)] public string info;
    }
}