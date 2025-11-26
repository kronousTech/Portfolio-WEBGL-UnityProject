using KronosTech.Room.ContentViewer;
using System.Collections.Generic;
using UnityEngine;

namespace KronosTech.Data
{
    [CreateAssetMenu(fileName = "RoomData", menuName = "Scriptable Objects/RoomData")]
    public class RoomData : ScriptableObject
    {
        public string ProjectName;
        public string ClientName;
        public RoomTagFlags Tags;
        public List<InfoCategory> Texts;
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