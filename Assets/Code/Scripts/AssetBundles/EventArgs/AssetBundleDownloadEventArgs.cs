using System;

namespace KronosTech.AssetBundles
{
    public class AssetBundleDownloadEventArgs : EventArgs
    {
        public bool IsSuccessful { get; }
        public bool AlreadyUpdated { get; }
        public string Error { get; }

        public AssetBundleDownloadEventArgs(string error)
        {
            IsSuccessful = false;
            AlreadyUpdated = false;
            Error = error;
        }
        public AssetBundleDownloadEventArgs()
        {
            IsSuccessful = true;
            AlreadyUpdated = true;
            Error = string.Empty;
        }
    }
}