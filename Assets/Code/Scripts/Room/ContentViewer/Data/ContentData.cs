using System;

namespace KronosTech.Room.ContentViewer
{
    [Serializable]
    public class ContentData<T>
    {
        public string Title;
        public T Data;
    }
}