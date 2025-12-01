using System;

namespace KronosTech.AssetBundles
{
    [Serializable]
    public struct AssetData
    {
        public string Bundle;
        public string Name;

        public AssetData(string name, string bundle)
        {
            this.Name = name;
            this.Bundle = bundle;
        }
    }
}