using KronosTech.Gallery.Rooms.ContentViewer;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Gallery.Rooms
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "Scriptable Objects/RoomData")]
    public class RoomData : ScriptableObject
    {
        public string ProjectName;
        public string ClientName;
        public RoomTagFlags Tags;
        public List<InfoCategoryData> Texts;
        public ContentDataHolderAssetData Images;
        public ContentDataHolderUrl Videos;
        public RoomClickableLinkData[] ClickableLinks;

        public string GetFullName()
        {
            return $"{ClientName}-{ProjectName}";
        }
        public bool HasVideos()
        {
            return Videos != null && Videos.Data != null && Videos.Data.Length > 0;
        }
    }
}