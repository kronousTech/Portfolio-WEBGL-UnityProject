using System;

namespace KronosTech.AssetBundles
{
    public class AssetBundleLoadEventArgs<T> : EventArgs
    {
        public bool IsSuccessful { get; }
        public T Asset { get; }
        public string Error { get; }

        public AssetBundleLoadEventArgs(string error)
        {
            IsSuccessful = false;
            Asset = default;
            Error = error;
        }
        public AssetBundleLoadEventArgs(T asset)
        {
            IsSuccessful = true;
            Asset = asset;
            Error = string.Empty;
        }
    }
}