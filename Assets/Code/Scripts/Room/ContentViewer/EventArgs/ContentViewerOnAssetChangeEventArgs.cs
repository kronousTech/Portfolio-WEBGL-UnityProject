using System;

namespace KronosTech.Room.ContentViewer
{
    public class ContentViewerOnAssetChangeEventArgs : EventArgs
    {
        public int Index { get; }
        public string Title { get; }

        public ContentViewerOnAssetChangeEventArgs(int index, string title)
        {
            Index = index;
            Title = title;
        }
    }
}