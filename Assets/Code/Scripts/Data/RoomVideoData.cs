namespace KronosTech.Data
{
    public readonly struct RoomVideoData
    {
        public string Title { get; }
        public string Url { get; }

        public RoomVideoData(string title, string url)
        {
            Title = title; 
            Url = url;
        }
    }
}