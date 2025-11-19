using System;
using UnityEngine.Networking;

namespace KronosTech.WebRequests
{
    public class WebRequestEventArgs : EventArgs
    {
        public bool IsSuccessful { get; }
        public DownloadHandler Handler { get; }
        public string Error { get; }

        public WebRequestEventArgs(string error)
        {
            IsSuccessful = false;
            Handler = null;
            Error = error;
        }
        public WebRequestEventArgs(DownloadHandler buffer)
        {
            IsSuccessful = true;
            Handler = buffer;
            Error = string.Empty;
        }
    }
}