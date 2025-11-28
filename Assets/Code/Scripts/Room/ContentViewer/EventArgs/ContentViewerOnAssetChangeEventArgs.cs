using System;

namespace KronosTech.Room.ContentViewer
{
    public class ContentViewerOnAssetChangeEventArgs : EventArgs
    {
        public int Index { get; }
        public ContentData Data { get; }

        public ContentViewerOnAssetChangeEventArgs(int index, ContentData data)
        {
            Index = index;
            Data = data;
        }
    }
}