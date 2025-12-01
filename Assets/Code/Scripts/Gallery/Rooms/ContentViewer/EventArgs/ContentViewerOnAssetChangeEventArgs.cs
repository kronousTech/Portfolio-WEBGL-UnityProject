using KronosTech.Gallery.Rooms.ContentViewer.Data;
using System;

namespace KronosTech.Gallery.Rooms.ContentViewer
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