using UnityEngine.Video;

namespace KronosTech.Data
{
    public readonly struct RoomVideoData
    {
        public string Title { get; }
        public VideoClip VideoClip { get; }

        public RoomVideoData(string title, VideoClip clip)
        {
            Title = title; 
            VideoClip = clip;
        }
    }
}